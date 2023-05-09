﻿using System.Collections.Immutable;
using System.Text;
using EnumClass.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace EnumClass.Generator;

[Generator(LanguageNames.CSharp)]
public class EnumClassIncrementalGenerator: IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext generatorContext)
    {
        IncrementalValuesProvider<EnumDeclarationSyntax> enums = 
            generatorContext
               .SyntaxProvider
               .CreateSyntaxProvider(
                    predicate: (node, _) => node is EnumDeclarationSyntax {AttributeLists.Count: > 0}, 
                    transform: GetSemanticModelForEnumClass)
               .Where(x => x is not null)!;

        var provider = generatorContext.CompilationProvider.Combine(enums.Collect());
        generatorContext.RegisterSourceOutput(provider, (context, tuple) => GenerateAllEnumClasses(tuple.Left, tuple.Right, context));
    }

    private static void GenerateAllEnumClasses(Compilation                           compilation,
                                               ImmutableArray<EnumDeclarationSyntax> enums,
                                               SourceProductionContext               context)
    {
        // Why do i need to compile? Skip!
        if (enums.IsDefaultOrEmpty)
        {
            return;
        }

        // Extract all EnumInfo from found syntax list
        var enumInfos = Helpers.GetAllEnumsToGenerate(compilation, enums, context.CancellationToken);
        
        // Return if EnumInfos not found
        if (enumInfos is null or {Count: 0})
        {
            return;
        }
        
        var nullableEnabled = compilation.Options.NullableContextOptions is not NullableContextOptions.Disable;
        var builder   = new StringBuilder();
        foreach (var enumInfo in enumInfos)
        {
            builder.Clear();
            if (nullableEnabled)
            {
                // Source generated files should contain directive
                builder.Append("#nullable enable\n\n");
            }
            builder.AppendLine("using System;");
            builder.AppendLine("using System.Runtime.CompilerServices;");
            builder.AppendLine();
            builder.AppendFormat("namespace {0}\n{{\n", enumInfo.Namespace);
            builder.AppendLine();
            builder.AppendFormat("public abstract partial class {0}: "
                               + "IEquatable<{0}>, IEquatable<{1}>, "
                               + "IComparable<{0}>, IComparable<{1}>, IComparable\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("{");
            // Field of original enum we are wrapping
            builder.AppendFormat("    protected readonly {0} _realEnumValue;\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine();
// Use for generating record init properties
            // Constructor to initialize wrapped enum
            builder.AppendFormat("    protected {0}({1} enumValue)\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("    {");
            builder.AppendLine("        this._realEnumValue = enumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();

            // Cast to original enum
            builder.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.AppendFormat("    public static implicit operator {0}({1} value)\n", enumInfo.FullyQualifiedEnumName, enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        return value._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Cast to integer 
            builder.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.AppendFormat("    public static explicit operator {0}({1} value)\n", enumInfo.UnderlyingType.CSharpKeyword, enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendFormat("        return ({0}) value._realEnumValue;\n", enumInfo.UnderlyingType.CSharpKeyword);
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // IEquatable for enum class
            builder.AppendFormat(nullableEnabled
                                     ? "    public bool Equals({0}? other)\n"
                                     : "    public bool Equals({0} other)\n", enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        return !ReferenceEquals(other, null) && other._realEnumValue == this._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();

            // IEquatable for original enum
            builder.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.AppendFormat("    public bool Equals({0} other)\n", enumInfo.FullyQualifiedEnumName);
            
            builder.AppendLine("    {");
            builder.AppendLine("        return other == this._realEnumValue;");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // Generic equals override using IEquatable<> interfaces
            builder.AppendLine(nullableEnabled
                                   ? "    public override bool Equals(object? other)"
                                   : "    public override bool Equals(object other)");
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
            builder.AppendLine("    [MethodImpl(MethodImplOptions.AggressiveInlining)]");
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
                builder.AppendLine($"        {enumVariableName} = null;");
                builder.AppendLine("        return false;");
                builder.AppendLine("    }\n");
            }
            builder.AppendLine();
            
            // Implementations for IComparable interfaces
            
            // Enums implement IComparable.Compare(object) so there is allocation of value type (enum).
            // Comparison by subtracting integral values has same semantics while remaining a faster option
            
            // IComparable<object>
            builder.AppendLine(nullableEnabled 
                                   ? "    public int CompareTo(object? other)" 
                                   : "    public int CompareTo(object other)");
            builder.AppendLine("    {");
            builder.AppendLine("        if (ReferenceEquals(this, other)) return 0;");
            builder.AppendLine("        if (ReferenceEquals(null, other)) return 1;");
            builder.AppendFormat("        if (other is {0})\n", enumInfo.ClassName);
            builder.AppendLine("        {");
            builder.AppendFormat("            {0} temp = ({0}) other;\n", enumInfo.ClassName);
            builder.AppendFormat("            {0} result = (({0})this._realEnumValue) - (({0}) temp._realEnumValue);\n",
                enumInfo.UnderlyingType.CSharpKeyword);
            builder.AppendLine("            return result < 0 ? -1 : result == 0 ? 0 : 1;");
            builder.AppendLine("        }");
            // Cast passed object directly to int bypassing casting to original enum
            builder.AppendFormat("        if (other is {0})\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("        {");
            builder.AppendFormat("            {0} result = (({0})this._realEnumValue) - (({0}) other);\n",
                enumInfo.UnderlyingType.CSharpKeyword);
            builder.AppendLine("            return result < 0 ? -1 : result == 0 ? 0 : 1;");
            builder.AppendLine("        }");
            builder.AppendLine($"        throw new ArgumentException($\"Object to compare must be either {{typeof({enumInfo.ClassName})}} or {{typeof({enumInfo.FullyQualifiedEnumName})}}. Given type: {{other.GetType()}}\", \"other\");");
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // IComparable<EnumClass>
            builder.AppendFormat(nullableEnabled 
                                   ? "    public int CompareTo({0}? other)\n" 
                                   : "    public int CompareTo({0} other)\n", enumInfo.ClassName);
            builder.AppendLine("    {");
            builder.AppendLine("        if (ReferenceEquals(this, other)) return 0;");
            builder.AppendLine("        if (ReferenceEquals(null, other)) return 1;");
            builder.AppendFormat("            {0} result = (({0})this._realEnumValue) - (({0}) other._realEnumValue);\n",
                enumInfo.UnderlyingType.CSharpKeyword);
            builder.AppendLine("            return result < 0 ? -1 : result == 0 ? 0 : 1;");
            // builder.AppendFormat("        return (({0})this._realEnumValue) - (({0}) other._realEnumValue);\n", enumInfo.UnderlyingType);
            builder.AppendLine("    }");
            builder.AppendLine();
            
            // IComparable<Enum>
            builder.AppendFormat("    public int CompareTo({0} other)\n", enumInfo.FullyQualifiedEnumName);
            builder.AppendLine("    {");
            // builder.AppendFormat("        return (({0})this._realEnumValue) - (({0}) other);", enumInfo.UnderlyingType);
            builder.AppendFormat("            {0} result = (({0})this._realEnumValue) - (({0}) other);\n",
                enumInfo.UnderlyingType.CSharpKeyword);
            builder.AppendLine("            return result < 0 ? -1 : result == 0 ? 0 : 1;");
            builder.AppendLine("    }");
            builder.AppendLine();

            
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
                
                // Generate Switches 
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


                builder.AppendLine("        [MethodImpl(MethodImplOptions.AggressiveInlining)]");
                builder.AppendLine("        public override int GetHashCode()");
                builder.AppendLine("        {");
                builder.AppendFormat("            return {0};\n", enumInfo.UnderlyingType.ComputeHashCode(member.IntegralValue));
                builder.AppendLine("        }");
                
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

                // Constructor is the method
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }
                
                // Check attribute name with plain text,
                // because now we do not have access to ISymbol of Attribute
                var containingTypeSymbol = attributeSymbol.ContainingType?
                                                          .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                                                          .Replace("global::", "");
                if (containingTypeSymbol is not Constants.EnumClassAttributeInfo.AttributeFullName)
                {
                    continue;
                }

                return syntax;
            }
        }

        return null;
    }
}