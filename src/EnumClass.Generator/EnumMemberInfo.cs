using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator;

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
    /// This is without 'global::' prefix
    /// </summary>
    public string EnumMemberName { get; }
    private EnumMemberInfo(string className,
                           string fullyQualifiedClassName,
                           string fullyQualifiedEnumValue,
                           string enumMemberName,
                           string stringRepresentation)
    {
        ClassName = className;
        FullyQualifiedClassName = fullyQualifiedClassName; 
        FullyQualifiedEnumValue = fullyQualifiedEnumValue;
        EnumMemberName = enumMemberName;
        _stringRepresentation = stringRepresentation;
    }

    /// <summary>
    /// Create enum value info with passed 'raw' values
    /// </summary>
    /// <returns>Instance of created enum value info</returns>
    public static EnumMemberInfo? CreateFromFieldSymbol(IFieldSymbol fieldSymbol, INamedTypeSymbol? stringRepresentationAttribute)
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
                
        return new EnumMemberInfo(className, fullyQualifiedClassName, fullyQualifiedEnumValue, enumMemberName, stringRepresentation);

        string GetToStringFromValue()
        {
            // Do not search if no attributes found
            if (stringRepresentationAttribute is not null && 
                fieldSymbol.GetAttributes() is {Length: >0} attributes)
            {
                return attributes
                           // Filter only [StringValue("EnumValueString")] attribute
                           // with single primitive (string) argument
                          .FirstOrDefault(data => data.ConstructorArguments is {Length: 1}
                                               && IsStringValueAttribute(data)
                                               && data.ConstructorArguments[0] is
                                                  {
                                                      IsNull: false,
                                                      Kind: TypedConstantKind.Primitive
                                                  } constant
                                               && constant.Value?.ToString() is
                                                  {
                                                      Length: >0
                                                  } stringValue
                                               && !string.IsNullOrWhiteSpace(stringValue)) is {} x 
                           ? x.ConstructorArguments[0].Value!.ToString().Trim() 
                           : fieldSymbol.Name;
            }

            return fieldSymbol.Name;
            
            bool IsStringValueAttribute(AttributeData attributeData)
            {
                return SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, stringRepresentationAttribute);
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
        var firstLetter = char.ToLowerInvariant(EnumMemberName[0]);
        var switchArgName = $"{firstLetter}{EnumMemberName.Substring(1)}Switch";
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