using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Generator;

internal static class Helpers
{
    /// <summary>
    /// Create EnumInfo domain object representing enum class with dependencies (names,  members etc)
    /// </summary>
    /// <param name="compilation">Compilation context, where <paramref name="enums"/> were found</param>
    /// <param name="enums">Enum declarations with [EnumClass] attribute on them</param>
    /// <param name="ct">Cancellation token from user compilation</param>
    /// <returns>List of <see cref="EnumInfo"/> with length > 0 if successfully parsed, or null otherwise</returns>
    public static List<EnumInfo>? GetAllEnumsToGenerate(Compilation                           compilation,
                                                        ImmutableArray<EnumDeclarationSyntax> enums,
                                                        CancellationToken                     ct)
    {
        var enumClassAttributeSymbol = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeFullName);
        if (enumClassAttributeSymbol is null)
        {
            return null;
        }
        var enumInfos = new List<EnumInfo>(enums.Length);

        var enumMemberInfoAttribute = compilation.GetTypeByMetadataName(Constants.EnumMemberInfoAttributeFullName);
        
        foreach (var syntax in enums)
        {
            // Do check twice if we get cancel request in between creating EnumInfo
            // Single check might fail if 'enums' contains single element and
            // cancellation happened while creating EnumInfo
            ct.ThrowIfCancellationRequested();
            var enumInfo = EnumInfo.CreateFromDeclaration(syntax, 
                compilation, 
                enumClassAttributeSymbol,
                enumMemberInfoAttribute);
            ct.ThrowIfCancellationRequested();
            if (enumInfo is not null)
            {
                enumInfos.Add(enumInfo);
            }
        }

        return enumInfos.Count > 0 ? enumInfos : null;
    }
}