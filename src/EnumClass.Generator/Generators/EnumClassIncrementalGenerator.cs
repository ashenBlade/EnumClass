using System.Collections.Immutable;
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
        var nullableEnabled = compilation.Options.NullableContextOptions is not NullableContextOptions.Disable;
        foreach (var enumInfo in enumInfos)
        {
            builder.Clear();
            if (nullableEnabled)
            {
                builder.Append("#nullable enable\n\n");
            }
            builder.AppendLine("using System;");
            builder.AppendLine("using System.Runtime.CompilerServices;");
            builder.AppendLine();
            builder.AppendFormat("namespace {0}.EnumClass\n{{\n", enumInfo.Namespace);
            builder.AppendLine();
            builder.AppendFormat("public abstract partial class {0}: IEquatable<{0}>, IEquatable<{1}>\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("{");
            builder.AppendFormat("    protected readonly {0} _realEnumValue;\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine();

            builder.AppendFormat("    protected {0}({1} enumValue)\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("    {");
            builder.AppendLine("        this._realEnumValue = enumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();

            // Cast to original enum
            builder.AppendFormat("    public static implicit operator {0}({1} value)\n", enumInfo.FullyQualifiedEnumName, enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        return value._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Cast to integer 
            builder.AppendFormat("    public static explicit operator int({0} value)\n", enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        return (int) value._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // IEquatable for enum class
            if (nullableEnabled)
            {
                builder.AppendFormat("    public bool Equals({0}? other)\n", enumInfo.ClassName);
            }
            else
            {
                builder.AppendFormat("    public bool Equals({0} other)\n", enumInfo.ClassName);
            }
            builder.AppendLine("    {");
            builder.AppendLine("        return !ReferenceEquals(other, null) && other._realEnumValue == this._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();

            // IEquatable for original enum
            builder.AppendFormat("    public bool Equals({0} other)\n", enumInfo.FullyQualifiedEnumName);
            
            builder.AppendLine("    {");
            builder.AppendLine("        return other == this._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Generic equals override using IEquatable<> interfaces
            if (nullableEnabled)
            {
                builder.AppendLine("    public override bool Equals(object? other)");
            }
            else
            {
                builder.AppendLine("    public override bool Equals(object other)");
            }
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
            builder.AppendLine("        return this._realEnumValue.GetHashCode();");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Generate TryParse
            {
                var enumVariableName = enumInfo.GetVariableName();
                builder.AppendLine(nullableEnabled
                                       ? $"    public static bool TryParse(string value, out {enumInfo.ClassName}? {enumVariableName})"
                                       : $"    public static bool TryParse(string value, out {enumInfo.ClassName} {enumVariableName})");
                builder.AppendLine("    {");
                builder.AppendLine("        switch (value)");
                builder.AppendLine("        {");

                // First, check for only enum member name. 
                // Then when enum name added.
                // We do it in that way (not merging with enum name together),
                // because usually we have only enum member name in string (my subjective opinion)
                foreach (var member in enumInfo.Members)
                {
                    builder.Append($"            case \"{member.EnumMemberNameOnly}\":\n");
                    builder.Append($"                {enumVariableName} = {member.EnumMemberNameOnly};\n");
                    builder.Append($"                return true;\n");
                }
                foreach (var member in enumInfo.Members)
                {
                    builder.Append($"            case \"{member.EnumMemberNameWithEnumName}\":\n");
                    builder.Append($"                {enumVariableName} = {member.EnumMemberNameOnly};\n");
                    builder.Append($"                return true;\n");
                }
                
                builder.AppendLine("        }");
                builder.AppendLine(nullableEnabled
                                       ? $"        {enumVariableName} = null!;"
                                       : $"        {enumVariableName} = null;");
                builder.AppendLine("        return false;");
                builder.AppendLine("    }\n");
            }

            // Generate Switch definitions
            var maxArgsCount = 8;
            for (var i = 0; i < maxArgsCount; i++)
            {
                builder.AppendFormat("    public abstract {0};\n", enumInfo.GenerateSwitchActionDefinition(i));
                builder.AppendFormat("    public abstract {0};\n", enumInfo.GenerateSwitchFuncDefinition(i));
            }
            
            // Generate subclasses for each member of enum
            foreach (var member in enumInfo.Members)
            {
                builder.AppendLine();
                // Generate static field for required Enum
                builder.AppendFormat("    public static readonly {0} {1} = new {0}();\n", member.ClassName, member.EnumMemberNameOnly);
            
                // Generate enum class for enum
                builder.AppendFormat("    public partial class {0}: {1}\n", member.ClassName, enumInfo.ClassName);
                builder.AppendLine("    {");
                
                // Generate constructor
                builder.AppendFormat("        public {0}(): base({1}) {{ }}\n", member.ClassName, member.FullyQualifiedEnumValue);
                
                // Override default ToString() 
                builder.AppendLine("        public override string ToString()");
                builder.AppendLine("        {");
                builder.AppendFormat("            return {0};\n", member.GetStringRepresentationQuoted());
                builder.AppendLine("        }");
                builder.AppendLine();
                
                // Generate Switch'es 
                for (var i = 0; i < maxArgsCount; i++)
                {
                    // Action<>
                    builder.AppendFormat("        public override {0}\n", enumInfo.GenerateSwitchActionDefinition(i));
                    builder.AppendLine("        {");
                    
                    builder.AppendFormat("            {0}(this", member.GetSwitchArgName());
                    for (var j = 0; j < i; j++)
                    {
                        builder.AppendFormat(", arg{0}", j);
                    }

                    builder.AppendLine(");");
                    builder.AppendLine("        }");
                    builder.AppendLine();
                    
                    // Func<>
                    builder.AppendFormat("        public override {0}\n", enumInfo.GenerateSwitchFuncDefinition(i));
                    builder.AppendLine("        {");
                    builder.AppendFormat("            return {0}(this", member.GetSwitchArgName());
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

            // Enum class
            builder.AppendLine("}");
            
            // Namespace
            builder.AppendLine("}");
            
            // Create source file
            context.AddSource($"{enumInfo.ClassName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
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

        var stringValueAttribute = compilation.GetTypeByMetadataName(Constants.StringRepresentationAttributeFullName);

        foreach (var syntax in enums)
        {
            // Do check twice if we get cancel request in between creating EnumInfo
            // Single check might fail if 'enums' contains single element and
            // cancellation happened while creating EnumInfo
            ct.ThrowIfCancellationRequested();
            var enumInfo = EnumInfo.CreateFromDeclaration(syntax, compilation, stringValueAttribute);
            ct.ThrowIfCancellationRequested();
            if (enumInfo is not null)
            {
                enumInfos.Add(enumInfo);
            }
        }

        return enumInfos;
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