using System.Text;
using EnumClass.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace EnumClass.Generator;

public static class GeneratorHelpers
{
    public static void GenerateEnumClass(EnumInfo enumInfo, 
                                         SourceProductionContext productionContext,
                                         GenerationContext context)
    {
        var nullableEnabled = context.NullableEnabled;
        var builder = context.GetBuilder();
        
        if (nullableEnabled)
        {
            // Source generated files should contain directive
            builder.Append("#nullable enable\n\n");
        }
        builder.AppendLine("using System;");
        builder.AppendLine("using System.Collections.Generic;");
        builder.AppendLine("using System.Runtime.CompilerServices;");
        builder.AppendLine();
        builder.AppendFormat("namespace {0}\n{{\n", enumInfo.Namespace);
        builder.AppendLine();
        builder.AppendFormat("{2} abstract partial class {0}: "
                           + "IEquatable<{0}>, IEquatable<{1}>, "
                           + "IComparable<{0}>, IComparable<{1}>, IComparable\n", enumInfo.ClassName, enumInfo.FullyQualifiedEnumName, enumInfo.Accessibility.Keyword);
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
            
        // Generate TryParse for string representation
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
                builder.Append($"            case \"{member.MemberName}\":\n");
                builder.Append($"                {enumVariableName} = {member.MemberName};\n");
                builder.Append($"                return true;\n");
            }
            foreach (var member in enumInfo.Members)
            {
                builder.Append($"            case \"{member.EnumMemberNameWithEnumName}\":\n");
                builder.Append($"                {enumVariableName} = {member.MemberName};\n");
                builder.Append($"                return true;\n");
            }
                
            builder.AppendLine("        }");
            builder.AppendLine($"        {enumVariableName} = null;");
            builder.AppendLine("        return false;");
            builder.AppendLine("    }\n");
        }
        builder.AppendLine();
            
        // Generate TryParse for integral value
        // Generate TryParse for string representation
        {
            var enumVariableName = enumInfo.GetVariableName();
            builder.AppendLine(nullableEnabled
                                   ? $"    public static bool TryParse({enumInfo.UnderlyingType.CSharpKeyword} value, out {enumInfo.ClassName}? {enumVariableName})"
                                   : $"    public static bool TryParse({enumInfo.UnderlyingType.CSharpKeyword} value, out {enumInfo.ClassName} {enumVariableName})");
            builder.AppendLine("    {");
            builder.AppendLine("        switch (value)");
            builder.AppendLine("        {");

            // First, check for only enum member name. 
            // Then when enum name added.
            // We do it in that way (not merging with enum name together),
            // because usually we have only enum member name in string (my subjective opinion)
            foreach (var member in enumInfo.Members)
            {
                builder.Append($"            case {member.IntegralValue}:\n");
                builder.Append($"                {enumVariableName} = {member.MemberName};\n");
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
        // Comparison by integral values has same semantics while remaining a faster option.
        // Do not use subtraction as it may lead to overflow

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
        builder.AppendFormat("            {0} left = (({0})this._realEnumValue);\n",
            enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendFormat("            {0} right = (({0})temp._realEnumValue);\n",
            enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendLine("            return left < right ? -1 : left == right ? 0 : 1;");
        builder.AppendLine("        }");
        // Cast passed object directly to int bypassing casting to original enum
        builder.AppendFormat("        if (other is {0})\n", enumInfo.FullyQualifiedEnumName);
        builder.AppendLine("        {");
        builder.AppendFormat("            {0} left = (({0})this._realEnumValue);\n", enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendFormat("            {0} right = (({0})other);\n", enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendLine("            return left < right ? -1 : left == right ? 0 : 1;");
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
        builder.AppendFormat("            {0} left = (({0})this._realEnumValue);\n",
            enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendFormat("            {0} right = (({0})other._realEnumValue);\n",
            enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendLine("            return left < right ? -1 : left == right ? 0 : 1;");
        builder.AppendLine("    }");
        builder.AppendLine();
            
        // IComparable<Enum>
        builder.AppendFormat("    public int CompareTo({0} other)\n", enumInfo.FullyQualifiedEnumName);
        builder.AppendLine("    {");
        builder.AppendFormat("            {0} left = (({0})this._realEnumValue);\n", enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendFormat("            {0} right = (({0})other);\n", enumInfo.UnderlyingType.CSharpKeyword);
        builder.AppendLine("            return left < right ? -1 : left == right ? 0 : 1;");
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
            builder.AppendFormat("    public static readonly {0} {1} = new {0}();\n", member.ClassName, member.MemberName);
            
            // Generate enum class for enum
            builder.AppendFormat("    public partial class {0}: {1}\n", member.ClassName, enumInfo.ClassName);
            builder.AppendLine("    {");
                
            // Generate constructor
            builder.AppendFormat("        public {0}(): base({1}) {{ }}\n", member.ClassName, member.FullyQualifiedEnumMemberName);
                
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

        builder.AppendLine();
            
        // Generate method for iterating over all instances
        builder.AppendFormat("    private static readonly {0}[] _members = new {0}[{1}] {{ ", enumInfo.ClassName, enumInfo.Members.Length);
            
        foreach (var member in enumInfo.Members)
        {
            builder.AppendFormat("{0}, ", member.MemberName);
        }

        builder.AppendLine("};\n");
            
        builder.AppendFormat("    public static System.Collections.Generic.IReadOnlyCollection<{0}> GetAllMembers()\n", enumInfo.ClassName);
        builder.AppendLine("    {");
        builder.AppendLine("        return _members;");
        builder.AppendLine("    }");

        // Enum class
        builder.AppendLine("}");
            
        // Namespace
        builder.AppendLine("}");
            
        // Create source file
        productionContext.AddSource($"{enumInfo.ClassName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
    }

}