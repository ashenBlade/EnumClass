using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Generator.Generators;

[Generator]
public class EnumClassIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext generatorContext)
    {
        IncrementalValuesProvider<EnumDeclarationSyntax> enums = 
            generatorContext
               .SyntaxProvider
               .CreateSyntaxProvider(FilterEnumDeclarations, GetSemanticModelForEnumClass)
               .Where(x => x is not null)!;

        var provider = generatorContext.CompilationProvider.Combine(enums.Collect());
        generatorContext.RegisterSourceOutput(provider, (context, tuple) => GenerateAllEnumClasses(tuple.Left, tuple.Right, context));
    }

    private static void GenerateAllEnumClasses(Compilation compilation,
                                               ImmutableArray<EnumDeclarationSyntax> enums,
                                               SourceProductionContext context)
    {
        if (enums.IsDefaultOrEmpty)
        {
            return;
        }

        var enumInfos = GetTypesToGenerate(compilation, enums, context.CancellationToken);
        var builder = new StringBuilder();
        foreach (var enumInfo in enumInfos)
        {
            builder.Clear();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendFormat("namespace {0}.EnumClass;", enumInfo.Namespace);
            builder.AppendLine();
            builder.AppendFormat("public abstract partial class {0}: IEquatable<{0}>, IEquatable<{1}>\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("{");
            builder.AppendLine("    public abstract int Value { get; }");
            builder.AppendFormat("    public abstract {0} Enum {{ get; }}\n", enumInfo.FullyQualifiedEnumName);
            
            // Cast to original enum
            builder.AppendFormat("    public static implicit operator {0}({1} value)\n", enumInfo.FullyQualifiedEnumName, enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        return value.Enum;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // IEquatable for enum class
            builder.AppendFormat("    public bool Equals({0} other)\n", enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        return !ReferenceEquals(other, null) && other.Enum == this.Enum;");
            builder.AppendLine("    }");
            builder.AppendLine();

            // IEquatable for enum class
            builder.AppendFormat("    public bool Equals({0} other)\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("    {");
            builder.AppendLine("        return other == this.Enum;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Generic equals override using IEquatable<> interfaces
            builder.AppendLine("    public override bool Equals(object other)");
            builder.AppendLine("    {");
            // First check it is null or self
            builder.AppendLine("        if (ReferenceEquals(other, null)) return false;");
            builder.AppendLine("        if (ReferenceEquals(other, this)) return true;");
            // Second check it is enum class instance
            builder.AppendFormat("        if (other is {0})\n", enumInfo.ClassName);
            builder.AppendLine("        {");
            builder.AppendFormat("            return this.Equals(({0}) other);\n", enumInfo.ClassName);
            builder.AppendLine("        }");
            // Then check it is raw original enum
            builder.AppendFormat("        if (other is {0})\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("        {");
            builder.AppendFormat("            return this.Equals(({0}) other);\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("        }");
            // Otherwise return false
            builder.AppendLine("        return false;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Create ==/!= operators for right raw original enum
            builder.AppendFormat("    public static bool operator ==({0} left, {1} right)\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("    {");
            builder.AppendFormat("        return left.Equals(right);\n");
            builder.AppendLine("    }");
            builder.AppendLine();
            builder.AppendFormat("    public static bool operator !=({0} left, {1} right)\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("    {");
            builder.AppendFormat("        return !left.Equals(right);\n");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Create ==/!= operators for left raw original enum
            builder.AppendFormat("    public static bool operator ==({0} left, {1} right)\n", enumInfo.FullyQualifiedEnumName, enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendFormat("        return right.Equals(left);\n");
            builder.AppendLine("    }");
            builder.AppendLine();
            builder.AppendFormat("    public static bool operator !=({0} left, {1} right)\n", enumInfo.FullyQualifiedEnumName, enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendFormat("        return !right.Equals(left);\n");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Generate GetHashCode
            builder.AppendLine("    public override int GetHashCode()");
            builder.AppendLine("    {");
            builder.AppendLine("        return this.Value;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            foreach (var enumValue in enumInfo.Values)
            {
                // Generate static field for required Enum
                builder.AppendFormat("    public static readonly {0} {1} = new {0}();\n", enumValue.ClassName, enumValue.Name);
            
                // Generate enum class for enum
                builder.AppendFormat("    public partial class {0}: {1}\n", enumValue.ClassName, enumInfo.ClassName);
                builder.AppendLine("    {");
                
                // Override required abstract fields
                builder.AppendFormat("        public override int Value => {0};", enumValue.IntValue);
                builder.AppendFormat("        public override {0} Enum => {1};", enumInfo.FullyQualifiedEnumName, enumValue.FullyQualifiedName);
                builder.AppendLine();
                
                // Override default ToString() 
                builder.AppendLine("        public override string ToString()");
                builder.AppendLine("        {");
                builder.AppendFormat("            return {0};\n", enumValue.GetStringRepresentationQuoted());
                builder.AppendLine("        }");
                
                builder.AppendLine("    }");
            }

            builder.AppendLine("}");
            context.AddSource($"{enumInfo.ClassName}.g.cs", builder.ToString());
        }
    }
    
    private static List<EnumInfo> GetTypesToGenerate(Compilation compilation,
                                                     ImmutableArray<EnumDeclarationSyntax> enums,
                                                     CancellationToken ct)
    {
        var enumClassAttributeSymbol = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeFullName);
        var enumInfos = new List<EnumInfo>();
        if (enumClassAttributeSymbol is null)
        {
            return enumInfos;
        }

        foreach (var syntax in enums)
        {
            ct.ThrowIfCancellationRequested();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
            if (ModelExtensions.GetDeclaredSymbol(semanticModel, syntax) is not INamedTypeSymbol enumSymbol)
            {
                continue;
            }

            var @namespace = enumSymbol.ContainingNamespace.Name;
            var memberNames = enumSymbol.GetMembers()
                                        .Where(member => member is IFieldSymbol {ConstantValue: not null})
                                        .Select((e, i) => new EnumValueInfo(e.Name, enumSymbol.Name, @namespace, i + 1))
                                        .ToArray();

            enumInfos.Add(new EnumInfo(enumSymbol.Name, @namespace, memberNames));
        }

        return enumInfos;
    }
    
    private static bool FilterEnumDeclarations(SyntaxNode node, CancellationToken token)
    {
        return node is EnumDeclarationSyntax {AttributeLists.Count: > 0};
    }

    private static EnumDeclarationSyntax? GetSemanticModelForEnumClass(GeneratorSyntaxContext context, CancellationToken token)
    {
        var syntax = ( EnumDeclarationSyntax ) context.Node;
        var attributeLists = syntax.AttributeLists;
        for (var i = 0; i < attributeLists.Count; i++)
        {
            var attributes = attributeLists[i].Attributes;
            for (var j = 0; j < attributes.Count; j++)
            {
                var attributeSyntax = attributes[j];
                if (ModelExtensions.GetSymbolInfo(context.SemanticModel, attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                var containingTypeSymbol = attributeSymbol.ContainingType?.ToDisplayString();
                if (containingTypeSymbol is not Constants.EnumClassAttributeFullName)
                {
                    continue;
                }

                return syntax;
            }
        }

        return null;
    }
}