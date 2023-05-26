using System.Linq;
using EnumClass.Core.Accessibility;
using EnumClass.Core.UnderlyingType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Core;

public static class EnumInfoFactory
{
    public static EnumInfo CreateFromNamedTypeSymbol(INamedTypeSymbol enumSymbol, 
                                                     INamedTypeSymbol enumClassAttribute, 
                                                     INamedTypeSymbol? enumMemberInfoAttribute)
    {
        var members = enumSymbol.GetMembers();
        
        var memberInfos = members
                          // Skip all non enum fields declarations
                         .OfType<IFieldSymbol>()
                          // Enum members are all const, according to docs
                         .Where(static m => m is {IsConst: true, HasConstantValue:true})
                          // Try to convert them into EnumMemberInfo
                         .Select(symbol => EnumMemberInfoFactory.CreateFromFieldSymbol(symbol, enumMemberInfoAttribute)!)
                          // And skip failed
                         .Where(static i => i is not null)
                          // Finally, create array of members
                         .ToArray();
        var attributeInfo = ExtractEnumClassAttributeCtorInfo(enumSymbol, enumClassAttribute);
        var fullyQualifiedEnumName = SymbolDisplay.ToDisplayString(enumSymbol, SymbolDisplayFormat.FullyQualifiedFormat);
        var className = GetClassName(enumSymbol, attributeInfo);
        var ns = GetResultNamespace(enumSymbol, attributeInfo);
        var underlyingType = GetUnderlyingType(enumSymbol);
        var accessibility = GetAccessibility(enumSymbol);
        return new EnumInfo(fullyQualifiedEnumName, className, ns, memberInfos, underlyingType, accessibility);
    }

    private static IAccessibility GetAccessibility(INamedTypeSymbol enumSymbol)
    {
        return GeneralAccessibility.FromAccessibility(enumSymbol.DeclaredAccessibility);
    }
    
    /// <summary>
    /// Get integral underlying type of enum
    /// </summary>
    /// <param name="enumSymbol">Symbol of original enum</param>
    /// <returns>Interface of underlying type</returns>
    private static IUnderlyingType GetUnderlyingType(INamedTypeSymbol enumSymbol)
    {
     // This can not be null because enumSymbol is enum
     // and for enum property EnumUnderlyingType must not be null
     return enumSymbol.EnumUnderlyingType!.Name switch
            {
                   
             "Int32"  => UnderlyingTypes.Int,
             "Byte"   => UnderlyingTypes.Byte,
             "Int16"  => UnderlyingTypes.Short, 
             "Int64"  => UnderlyingTypes.Long,
             "UInt64" => UnderlyingTypes.Ulong,
             "SByte"  => UnderlyingTypes.Sbyte, 
             "UInt16" => UnderlyingTypes.Ushort,
             "UInt32" => UnderlyingTypes.Uint,

             // Fallback.
             // Maybe better to throw exception or display diagnostic?
             _ => UnderlyingTypes.Int
            };
    }

    private static string GetClassName(INamedTypeSymbol enumSymbol, EnumClassAttributeInfo info)
    {
     return SymbolDisplay.FormatLiteral( info.TargetClassName ?? enumSymbol.Name, false );
    }

    private static string GetResultNamespace(INamedTypeSymbol enumSymbol, EnumClassAttributeInfo attributeInfo)
    {
     return attributeInfo.Namespace ?? enumSymbol.ContainingNamespace
                                                 .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                                                 .Replace("global::", "") + ".EnumClass";
    }
    
    /// <summary>
    /// Method to extract information from [EnumClass] attribute: properties, arguments etc...
    /// </summary>
    private static EnumClassAttributeInfo ExtractEnumClassAttributeCtorInfo(INamedTypeSymbol enumSymbol,
                                                                            INamedTypeSymbol enumClassAttribute)
    {
        var info = new EnumClassAttributeInfo();
        
        // Search for [EnumClass] with at least 1 set property
        if (enumSymbol.GetAttributes() is {Length:>0} attributes && 
            attributes.FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, enumClassAttribute)) is
                {
                    NamedArguments.Length: >0
                } 
                attrInstance)
        {
            foreach (var namedArgument in attrInstance.NamedArguments)
            {
                // Prevent nulls
                if (namedArgument.Value is not
                    {
                        IsNull: false,
                        Kind: TypedConstantKind.Primitive,
                        Value: not null
                    } value)
                {
                    continue;
                }

                // For now we only have string primitives, so common function can be used.
                // May this will be changed in future
                switch (namedArgument.Key)
                {
                    case Constants.EnumClassAttributeInfo.NamedArguments.Namespace:
                        info = info with {Namespace = GetConstantStringValue(value)};
                        break;
                    case Constants.EnumClassAttributeInfo.NamedArguments.TargetClassName:
                        info = info with {TargetClassName = GetConstantStringValue(value)};
                        break;
                }
            }
        }
        
        return info;

        string? GetConstantStringValue(TypedConstant constant)
        {
            return constant.Value?.ToString() is {Length: > 0} notEmptyString 
                && !string.IsNullOrWhiteSpace(notEmptyString)
                       ? notEmptyString
                       : null;
        }
    }

    /// <summary>
    /// Record that represents named arguments of [EnumClass] attribute
    /// </summary>
    private readonly record struct EnumClassAttributeInfo(string? Namespace, string? TargetClassName);

}