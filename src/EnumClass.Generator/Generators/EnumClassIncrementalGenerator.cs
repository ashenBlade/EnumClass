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
            
            // Generate switches definitions
            builder.AppendFormat("    public abstract {0};\n", enumInfo.GenerateSwitchActionDefinition(0));
            builder.AppendFormat("    public abstract {0};\n", enumInfo.GenerateSwitchFuncDefinition(0));
            
            foreach (var enumValue in enumInfo.Values)
            {
                // Generate static field for required Enum
                builder.AppendFormat("    public static readonly {0} {1} = new {0}();\n", enumValue.ClassName, enumValue.Name);
            
                // Generate enum class for enum
                builder.AppendFormat("    public partial class {0}: {1}\n", enumValue.ClassName, enumInfo.ClassName);
                builder.AppendLine("    {");
                
                // Override required abstract fields
                builder.AppendFormat("        public override int Value => {0};\n", enumValue.IntValue);
                builder.AppendFormat("        public override {0} Enum => {1};\n", enumInfo.FullyQualifiedEnumName, enumValue.FullyQualifiedName);
                builder.AppendLine();
                
                // Override default ToString() 
                builder.AppendLine("        public override string ToString()");
                builder.AppendLine("        {");
                builder.AppendFormat("            return {0};\n", enumValue.GetStringRepresentationQuoted());
                builder.AppendLine("        }");
                builder.AppendLine();
                
                // Generate zero in args count Action switch
                builder.AppendFormat("        public override {0}\n", enumInfo.GenerateSwitchActionDefinition(0));
                builder.AppendLine("        {");
                builder.AppendFormat("            {0}(this);\n", enumValue.GetSwitchArgName());
                builder.AppendLine("        }");
                builder.AppendLine();
                
                // Generate zero in args count Func switch
                builder.AppendFormat("        public override {0}\n", enumInfo.GenerateSwitchFuncDefinition(0));
                builder.AppendLine("        {");
                builder.AppendFormat("            return {0}(this);\n", enumValue.GetSwitchArgName());
                builder.AppendLine("        }");
                builder.AppendLine();

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
        var enumInfos = new List<EnumInfo>(enums.Length);
        if (enumClassAttributeSymbol is null)
        {
            return enumInfos;
        }

        var displayAttributeSymbol = compilation.GetTypeByMetadataName(Constants.DisplayAttributeFullName);

        foreach (var syntax in enums)
        {
            ct.ThrowIfCancellationRequested();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(syntax) is not { } enumSymbol)
            {
                continue;
            }

            var @namespace = enumSymbol.ContainingNamespace.Name;
            List<EnumValueInfo> list = new List<EnumValueInfo>();
            var currentOrdinalNumber = 0;
            foreach (var symbol in enumSymbol.GetMembers()
                                             .OfType<IFieldSymbol>()
                                             .Where(m => m.ConstantValue is not null))
            {
                string? name = null;
                
                // Try get name from [Display]
                if (displayAttributeSymbol is not null && 
                    symbol.GetAttributes()
                          .FirstOrDefault(a => SymbolEqualityComparer.IncludeNullability.Equals(a.AttributeClass, displayAttributeSymbol)) is {} attribute)
                {
                    // Try find name from Property arguments
                    name = attribute.NamedArguments
                                    .Where(argument => argument is
                                                       {
                                                           Key: "Name",
                                                           Value:
                                                           {
                                                               Kind: TypedConstantKind.Primitive,
                                                               IsNull: false,
                                                           }
                                                       })
                                    .Select(a => a.Value.Value!.ToString())
                                    .FirstOrDefault();
                }

                var value = currentOrdinalNumber;
                
                // Try determine next ordinal value from assigning operators
                if (symbol.HasConstantValue && 
                    symbol.ConstantValue is {} constantValue && 
                    int.TryParse(constantValue.ToString(), out var parsed))
                {
                    value = parsed;
                }
                
                var valueInfo = new EnumValueInfo(symbol.Name, enumSymbol.ToDisplayString(), value, name ?? symbol.Name);
                currentOrdinalNumber = value + 1;
                list.Add(valueInfo);
            }

            var memberNames = list
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