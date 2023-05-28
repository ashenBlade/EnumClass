using System;
using System.Linq;
using EnumClass.Core.Infrastructure;
using EnumClass.Core.Models;
using EnumClass.Core.SymbolName;
using Microsoft.CodeAnalysis;

namespace EnumClass.Core;

/// <summary>
/// Helper class to create EnumMemberInfo instances
/// </summary>
/// <remarks>
/// Only <see cref="EnumInfoFactory"/> is public, because <see cref="EnumMemberInfoFactory"/> is implementation detail
/// </remarks>
internal static class EnumMemberInfoFactory
{
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
        var enumClassName = $"{fieldSymbol.Name}EnumValue";
        var fullyQualifiedEnumClassName = $"{fieldSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}.EnumClass.{enumClassName}";
        
        var fullyQualifiedMemberName = $"{fieldSymbol.ContainingType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}.{fieldSymbol.Name}";
        var memberName = fieldSymbol.Name;
        
        var stringRepresentation = GetToStringFromValue();
        var enumMemberNameWithPrefix = $"{fieldSymbol.ContainingType.Name}.{fieldSymbol.Name}";
        var integralValue = fieldSymbol.ConstantValue?.ToString() ?? throw new ArgumentNullException();
        
        return new EnumMemberInfo(
            className: new ManuallySpecifiedSymbolName(fullyQualifiedEnumClassName, enumClassName),
            memberName: new ManuallySpecifiedSymbolName(fullyQualifiedMemberName, memberName),
            stringRepresentation,
            enumMemberNameWithPrefix, 
            integralValue);
        
        string GetToStringFromValue()
        {
            // If no attributes specified, fallback to name of member
            var attributes = fieldSymbol.GetAttributes();
            if (attributes is {Length: 0}) 
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
}