using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace EnumClass.Generator.Generators;

[Generator]
public class EnumClassIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext generatorContext)
    {
        RegisterAttributesOutput(generatorContext);
        
        IncrementalValuesProvider<EnumDeclarationSyntax> enums = 
            generatorContext
               .SyntaxProvider
               .CreateSyntaxProvider(FilterEnumDeclarations, GetSemanticModelForEnumClass)
               .Where(x => x is not null)!;

        var provider = generatorContext.CompilationProvider.Combine(enums.Collect());
        generatorContext.RegisterSourceOutput(provider, (context, tuple) => GenerateAllEnumClasses(tuple.Left, tuple.Right, context));
    }

    private static void RegisterAttributesOutput(IncrementalGeneratorInitializationContext generatorContext)
    {
        generatorContext.RegisterPostInitializationOutput(context =>
        {
            // Attribute made internal to not conflict with another existing attributes
            var enumClassAttributeCode = @"using System;

namespace EnumClass.Attributes
{
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    internal class EnumClassAttribute: Attribute
    { }
}";
            context.AddSource("EnumClassAttribute.g.cs", SourceText.From(enumClassAttributeCode, Encoding.UTF8));

            var stringRepresentationAttributeCode = @"using System;

namespace EnumClass.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class StringValueAttribute: Attribute
    {
        internal StringValueAttribute(string value)
        { }
    }
}";
            context.AddSource("StringValueAttribute.g.cs", SourceText.From(stringRepresentationAttributeCode, Encoding.UTF8));
        });
    }

    private static void GenerateAllEnumClasses(Compilation                           compilation,
                                               ImmutableArray<EnumDeclarationSyntax> enums,
                                               SourceProductionContext               context)
    {
        if (enums.IsDefaultOrEmpty)
        {
            return;
        }

        var enumInfos = GetTypesToGenerate(compilation, enums, context.CancellationToken);
        var builder   = new StringBuilder();
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
            var maxArgsCount = 8;
            for (var i = 0; i < maxArgsCount; i++)
            {
                builder.AppendFormat("    public abstract {0};\n", enumInfo.GenerateSwitchActionDefinition(i));
                builder.AppendFormat("    public abstract {0};\n", enumInfo.GenerateSwitchFuncDefinition(i));
            }
            
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
                
                for (var i = 0; i < maxArgsCount; i++)
                {
                    // Action
                    builder.AppendFormat("        public override {0}\n", enumInfo.GenerateSwitchActionDefinition(i));
                    builder.AppendLine("        {");
                    
                    builder.AppendFormat("            {0}(this", enumValue.GetSwitchArgName());
                    for (var j = 0; j < i; j++)
                    {
                        builder.AppendFormat(", arg{0}", j);
                    }

                    builder.AppendLine(");");
                    builder.AppendLine("        }");
                    builder.AppendLine();
                    
                    // Func
                    builder.AppendFormat("        public override {0}\n", enumInfo.GenerateSwitchFuncDefinition(i));
                    builder.AppendLine("        {");
                    builder.AppendFormat("            return {0}(this", enumValue.GetSwitchArgName());
                    for (var j = 0; j < i; j++)
                    {
                        builder.AppendFormat(", arg{0}", j);
                    }

                    builder.AppendLine(");");
                    builder.AppendLine("        }");
                    builder.AppendLine();
                }
                

                builder.AppendLine("    }");
            }

            builder.AppendLine("}");
            context.AddSource($"{enumInfo.ClassName}.g.cs", builder.ToString());
        }
    }
    
    private static List<EnumInfo> GetTypesToGenerate(Compilation                           compilation,
                                                     ImmutableArray<EnumDeclarationSyntax> enums,
                                                     CancellationToken                     ct)
    {
        var enumClassAttributeSymbol = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeFullName);
        var enumInfos                = new List<EnumInfo>(enums.Length);
        if (enumClassAttributeSymbol is null)
        {
            return enumInfos;
        }

        var stringRepresentationAttribute = compilation.GetTypeByMetadataName(Constants.StringRepresentationAttributeFullName);

        foreach (var syntax in enums)
        {
            ct.ThrowIfCancellationRequested();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(syntax) is not { } enumSymbol)
            {
                continue;
            }
            
            var @namespace           = enumSymbol.ContainingNamespace.Name;
            var list                 = new List<EnumValueInfo>();
            var currentOrdinalNumber = 0;
            foreach (var symbol in enumSymbol.GetMembers()
                                             .OfType<IFieldSymbol>()
                                             .Where(m => m.ConstantValue is not null))
            {
                var name = GetToStringEnumFieldValue(stringRepresentationAttribute, symbol);
                
                
                
                var value = currentOrdinalNumber;
                
                // Try determine next ordinal value from assigning operators
                if (symbol.HasConstantValue && 
                    symbol.ConstantValue is {} constantValue && 
                    int.TryParse(constantValue.ToString(), out var parsed))
                {
                    value = parsed;
                }
                
                var valueInfo = new EnumValueInfo(symbol.Name, enumSymbol.ToDisplayString(), value, name);
                currentOrdinalNumber = value + 1;
                list.Add(valueInfo);
            }

            var memberNames = list
               .ToArray();

            enumInfos.Add(new EnumInfo(enumSymbol.Name, @namespace, memberNames));
        }

        return enumInfos;
    }

    private static string GetToStringEnumFieldValue(INamedTypeSymbol? stringRepresentationAttribute, IFieldSymbol symbol)
    {
        bool IsStringValueAttribute(AttributeData attributeData)
        {
            return SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, stringRepresentationAttribute);
        }

        if (stringRepresentationAttribute is not null && 
            symbol.GetAttributes() is {Length: > 0} attributes)
        {
            foreach (var attributeData in attributes)
            {
                if (attributeData.ConstructorArguments is {Length:>0} constructorArguments && 
                    IsStringValueAttribute(attributeData))
                {
                    // It has only one constructor argument
                    if (constructorArguments.FirstOrDefault(arg => 
                                arg is
                                {
                                    IsNull:false, 
                                    Kind:TypedConstantKind.Primitive
                                }) 
                            is var constant
                     && constant.Value?.ToString() is {Length: > 0} toStringValue)
                    {
                        return toStringValue;
                    }
                }
            }
        }

        // Fallback value is it's name
        return symbol.Name;
    }

    private static bool FilterEnumDeclarations(SyntaxNode node, CancellationToken token)
    {
        return node is EnumDeclarationSyntax {AttributeLists.Count: > 0};
    }

    private static EnumDeclarationSyntax? GetSemanticModelForEnumClass(GeneratorSyntaxContext context, CancellationToken token)
    {
        var syntax         = ( EnumDeclarationSyntax ) context.Node;
        var attributeLists = syntax.AttributeLists;
        
        // Flatten all attribute lists
        for (var i = 0; i < attributeLists.Count; i++)
        {
            var attributes = attributeLists[i].Attributes;
            for (var j = 0; j < attributes.Count; j++)
            {
                var attributeSyntax = attributes[j];

                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
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