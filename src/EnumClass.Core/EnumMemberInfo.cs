#nullable enable
using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Core;

public class EnumMemberInfo
{
    private readonly string _stringRepresentation;
    
    /// <summary>
    /// Friendly name of class.
    /// To use in static fields for classes 
    /// </summary>
    public string ClassName { get; }
    
    /// <summary>
    /// Class name of enum class starting with 'global::' prefix
    /// </summary>
    public string FullyQualifiedClassName { get; }
    
    /// <summary>
    /// Name of enum value we constructing.
    /// This is fully qualified, with 'global::' prefix
    /// </summary>
    public string FullyQualifiedEnumValue { get; }

    /// <summary>
    /// Name of enum member we constructing.
    /// This is without 'global::' prefix and enum name
    /// </summary>
    /// <example>Cat</example>
    public string EnumMemberNameOnly { get; }

    /// <summary>
    /// Name of enum member with original enum name prefix.
    /// </summary>
    /// <example>PetKind.Cat</example>
    public string EnumMemberNameWithEnumName { get; }
    
    /// <summary>
    /// Integer value of enum.
    /// It can be int, byte, ulong etc.
    /// Thus named integral
    /// </summary>
    public string IntegralValue { get; }

    private EnumMemberInfo(string className,
                           string fullyQualifiedClassName,
                           string fullyQualifiedEnumValue,
                           string enumMemberNameOnly,
                           string stringRepresentation,
                           string enumMemberNameWithEnumName,
                           string integralValue)
    {
        ClassName = className;
        FullyQualifiedClassName = fullyQualifiedClassName; 
        FullyQualifiedEnumValue = fullyQualifiedEnumValue;
        EnumMemberNameOnly = enumMemberNameOnly;
        _stringRepresentation = stringRepresentation;
        EnumMemberNameWithEnumName = enumMemberNameWithEnumName;
        IntegralValue = integralValue;
    }

    /// <summary>
    /// Create enum value info with passed 'raw' values
    /// </summary>
    /// <returns>Instance of created enum value info</returns>
    public static EnumMemberInfo? CreateFromFieldSymbol(IFieldSymbol fieldSymbol, 
                                                        INamedTypeSymbol? enumMemberInfoAttribute)
    {
        // For enum member this must be true
        if (!fieldSymbol.IsConst)
        {
            return null;
        }

        // Class name is equivalent to enum member name
        var className = $"{fieldSymbol.Name}EnumValue";
        var fullyQualifiedClassName = $"{fieldSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}.EnumClass.{className}";
        
        var fullyQualifiedEnumValue = $"{fieldSymbol.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}.{fieldSymbol.Name}";
        var enumMemberName = fieldSymbol.Name;
        var stringRepresentation = GetToStringFromValue();
        var enumMemberNameWithPrefix = $"{fieldSymbol.ContainingType.Name}.{fieldSymbol.Name}";
        var integralValue = fieldSymbol.ConstantValue?.ToString() ?? throw new ArgumentNullException();
        
        return new EnumMemberInfo(className, 
            fullyQualifiedClassName, 
            fullyQualifiedEnumValue,
            enumMemberName,
            stringRepresentation,
            enumMemberNameWithPrefix, 
            integralValue);
        
        string GetToStringFromValue()
        {
            // If no attributes specified, fallback to name of member
            if (fieldSymbol.GetAttributes() is {Length: 0} attributes) 
                return fieldSymbol.Name;
            
            // Search string info in [EnumMemberInfo] 
            if (enumMemberInfoAttribute is not null
                // Find [EnumMemberInfo] attribute
             && attributes.FirstOrDefault(attr => attr.NamedArguments.Length > 0 && IsEnumMemberInfoAttribute(attr)) is {  } enumMemberAttr 
                // Check "StringValue" is set and it is correct
             && enumMemberAttr.NamedArguments
                              .FirstOrDefault(a => 
                                   a is
                                   {
                                       Key: Constants.EnumMemberInfoAttributeInfo.NamedArguments.StringValue,
                                       Value:
                                       {
                                           IsNull: false,
                                           Kind: TypedConstantKind.Primitive,
                                           Value: not null
                                       }
                                   }) is var value
                // Sanity checks
             && value.Value.Value!.ToString() is {Length:>0} notNullString 
             && !string.IsNullOrWhiteSpace(notNullString))
            {
                return notNullString;
            }
        

            // Fallback to member name
            return fieldSymbol.Name;

            bool IsEnumMemberInfoAttribute(AttributeData data)
            {
                return SymbolEqualityComparer.Default.Equals(data.AttributeClass, enumMemberInfoAttribute); 
            }
        }
    }
    
    /// <summary>
    /// Format string to represent switch function parameter name
    /// </summary>
    /// <returns>Argument name</returns>
    /// <example>CatEnumValue -> catEnumValueSwitch</example>
    public string GetSwitchArgName()
    {
        if (_switchArgName is not null)
        {
            return _switchArgName;
        }
        var firstLetter = char.ToLowerInvariant(EnumMemberNameOnly[0]);
        var switchArgName = $"{firstLetter}{EnumMemberNameOnly.Substring(1)}Switch";
        _switchArgName = switchArgName;
        return switchArgName;
    }

    /// <summary>
    /// Cached switch arg name
    /// </summary>
    private string? _switchArgName;

    /// <summary>
    /// Get <c>Action</c> switch type
    /// </summary>
    /// <param name="argsCount">Amount of arguments in type, including returning</param>
    /// <returns>Formatted type name</returns>
    public string GetActionSwitchType(int argsCount)
    {
        if (argsCount == 0)
        {
            return $"Action<{ClassName}>";
        }

        var builder = new StringBuilder($"Action<{ClassName}");
        for (int i = 0; i < argsCount; i++)
        {
            builder.Append(", T");
            builder.Append(i);
        }

        builder.Append(">");
        return builder.ToString();
    }

    /// <summary>
    /// Get <c>Func</c> switch type
    /// </summary>
    /// <param name="inArgsCount">Amount of arguments in type, including returning</param>
    /// <param name="resultGenericArgName">Generic name of return type</param>
    /// <returns>Formatted type name</returns>
    public string GetFuncSwitchType(int inArgsCount, string resultGenericArgName)
    {
        if (inArgsCount == 0)
        {
            return $"Func<{ClassName}, {resultGenericArgName}>";
        }

        var builder = new StringBuilder($"Func<{ClassName}");
        for (int i = 0; i < inArgsCount; i++)
        {
            builder.Append(", T");
            builder.Append(i);
        }

        builder.AppendFormat(", {0}>", resultGenericArgName);
        return builder.ToString();
    }
    
    public string GetStringRepresentationQuoted()
    {
        return SymbolDisplay.FormatLiteral(_stringRepresentation, true);
    }
}