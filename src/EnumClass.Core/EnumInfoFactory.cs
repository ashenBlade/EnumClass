using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EnumClass.Core.Accessibility;
using EnumClass.Core.Infrastructure;
using EnumClass.Core.Models;
using EnumClass.Core.SymbolName;
using EnumClass.Core.UnderlyingType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Core;

public static class EnumInfoFactory
{
    /// <summary>
    /// Extract EnumInfo from given <paramref name="enumSymbol"/>
    /// </summary>
    /// <param name="enumSymbol">Symbol represents enum</param>
    /// <param name="enumClassAttribute">EnumClassAttribute type symbol</param>
    /// <param name="enumMemberInfoAttribute">EnumMemberInfoAttribute type symbol</param>
    /// <returns>Extracted info from enum</returns>
    /// <remarks>
    /// Given <paramref name="enumSymbol"/> MUST be marked with [EnumInfo]
    /// </remarks>
    public static EnumInfo CreateFromNamedTypeSymbol(INamedTypeSymbol enumSymbol, 
                                                     INamedTypeSymbol enumClassAttribute, 
                                                     INamedTypeSymbol enumMemberInfoAttribute)
    {
        var members = enumSymbol.GetMembers();
        
        var attributeInfo = ExtractEnumClassAttributeCtorInfo(enumSymbol, enumClassAttribute);
        var underlyingType = GetUnderlyingType(enumSymbol);
        var accessibility = GetAccessibility(enumSymbol);
        
        var resultNamespace = GetResultNamespace(enumSymbol, attributeInfo.Namespace);
        var @namespace = new ManuallySpecifiedSymbolName($"global::{resultNamespace}", resultNamespace);
        
        var generatedClassName = GetClassName(enumSymbol, attributeInfo.ClassName);
        var fullyQualifiedClassName = $"global::{resultNamespace}.{generatedClassName}";
        var className = new ManuallySpecifiedSymbolName(fullyQualifiedClassName, generatedClassName);
        
        var fullyQualifiedEnumName = SymbolDisplay.ToDisplayString(enumSymbol, SymbolDisplayFormat.FullyQualifiedFormat);
        var enumName = new ManuallySpecifiedSymbolName(fullyQualifiedEnumName, enumSymbol.Name);

        var memberInfos = members
                         .OfType<IFieldSymbol>()
                         .Combine(new EnumMemberInfoCreationContext(className, @namespace, enumName))                 
                          // Skip all non enum fields declarations
                          // Enum members are all const, according to docs
                         .Where(static m => m.Left is {IsConst: true, HasConstantValue:true})
                          // Try to convert them into EnumMemberInfo
                         .Select(p => EnumMemberInfoFactory.CreateFromFieldSymbol(p.Left, p.Right, enumMemberInfoAttribute)!)
                          // And skip failed
                         .Where(static i => i is not null)
                          // Finally, create array of members
                         .ToArray();


        return new EnumInfo(
            className,
            enumName,
            memberInfos,
            underlyingType,
            accessibility,
            @namespace);
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

    private static string GetClassName(INamedTypeSymbol enumSymbol, string? userDefinedClassName)
    {
        return SymbolDisplay.FormatLiteral( userDefinedClassName ?? enumSymbol.Name, false );
    }

    private static string GetResultNamespace(INamedTypeSymbol enumSymbol, string? userDefinedNamespace)
    {
        return userDefinedNamespace ?? enumSymbol.ContainingNamespace
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
                    case Constants.EnumClassAttributeInfo.NamedArguments.ClassName:
                        info = info with {ClassName = GetConstantStringValue(value)};
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
    private readonly record struct EnumClassAttributeInfo(string? Namespace, string? ClassName);

    /// <summary>
    /// Find ALL enums marked with [EnumClassAttribute] from all assemblies accessible in compilation
    /// </summary>
    /// <param name="compilation">The compilation object is an immutable representation of a single invocation of the compiler</param>
    /// <param name="context">Context </param>
    /// <returns>All EnumInfo that were found and successfully extracted or <c>null</c> if error occured</returns>
    /// <remarks>
    /// Main consumers of this function are extension packages that create helper classes, such as JsonConverter
    /// </remarks>
    /// <remarks>
    /// If null returned, required diagnostics are already reported
    /// </remarks>
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    public static List<EnumInfo>? GetAllEnumInfosFromCompilation(Compilation compilation, 
                                                                 SourceProductionContext context)
    {
        var enumClassAttribute = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeInfo.AttributeFullName);
        var enumMemberInfoAttribute = compilation.GetTypeByMetadataName(Constants.EnumMemberInfoAttributeInfo.AttributeFullName);
        
        if (enumClassAttribute is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.NoEnumClassAttributeFound, Location.None));
            return null;
        }

        if (enumMemberInfoAttribute is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(Diagnostics.NoEnumMemberInfoAttributeFound, Location.None));
            return null;
        }

        var parsed = new List<EnumInfo>();
        
        foreach (var namedTypeSymbol in FactoryHelpers.ExtractAllEnumsFromCompilation(compilation))
        {
            if (IsMarkedWithEnumClassAttribute(namedTypeSymbol))
            {
                parsed.Add(CreateFromNamedTypeSymbol(namedTypeSymbol, enumClassAttribute, enumMemberInfoAttribute));
            }
        }

        return parsed;

        bool IsMarkedWithEnumClassAttribute(INamedTypeSymbol enumTypeSymbol)
        {
            foreach (var attribute in enumTypeSymbol.GetAttributes())
            {
                if (SymbolEqualityComparer.Default.Equals(attribute.AttributeClass, enumClassAttribute))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public static EnumInfo CreateFromExternalEnumNamedTypeSymbol(ExternalEnumClassAttributeInfo attributeInfo)
    {
        var enumSymbol = attributeInfo.EnumSymbol;
        var members = attributeInfo.EnumSymbol.GetMembers();
        
        var underlyingType = GetUnderlyingType(enumSymbol);
        var accessibility = GetAccessibility(enumSymbol);
        
        var resultNamespace = GetResultNamespace(enumSymbol, attributeInfo.Namespace);
        var @namespace = new ManuallySpecifiedSymbolName($"global::{resultNamespace}", resultNamespace);
        
        var generatedClassName = GetClassName(enumSymbol, attributeInfo.ClassName);
        var fullyQualifiedClassName = $"global::{resultNamespace}.{generatedClassName}";
        var className = new ManuallySpecifiedSymbolName(fullyQualifiedClassName, generatedClassName);
        
        var fullyQualifiedEnumName = SymbolDisplay.ToDisplayString(enumSymbol, SymbolDisplayFormat.FullyQualifiedFormat);
        var enumName = new ManuallySpecifiedSymbolName(fullyQualifiedEnumName, enumSymbol.Name);

        var memberInfos = members
                         .OfType<IFieldSymbol>()
                         .Combine(new EnumMemberInfoCreationContext(className, @namespace, enumName))                 
                          // Skip all non enum fields declarations
                          // Enum members are all const, according to docs
                         .Where(static m => m.Left is {IsConst: true, HasConstantValue:true})
                          // Try to convert them into EnumMemberInfo
                         .Select(p => EnumMemberInfoFactory.CreateFromFieldSymbol(p.Left, p.Right, null)!)
                          // And skip failed
                         .Where(static i => i is not null)
                          // Finally, create array of members
                         .ToArray();


        return new EnumInfo(
            className,
            enumName,
            memberInfos,
            underlyingType,
            accessibility,
            @namespace);
    }
}