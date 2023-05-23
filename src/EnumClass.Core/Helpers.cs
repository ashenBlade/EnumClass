using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Core;

public static class Helpers
{
    /// <summary>
    /// Create EnumInfo domain object representing enum class with dependencies (names,  members etc)
    /// </summary>
    /// <param name="compilation">Compilation context, where <paramref name="enums"/> were found</param>
    /// <param name="enums">All <c>enum</c> declarations. They will be filtered to be annotated with [EnumClass]</param>
    /// <param name="ct">Cancellation token from user compilation</param>
    /// <returns>List of <see cref="EnumInfo"/> with length > 0 if successfully parsed, or null otherwise</returns>
    public static List<EnumInfo>? GetAllEnumsToGenerate(Compilation                           compilation,
                                                        ImmutableArray<EnumDeclarationSyntax> enums,
                                                        CancellationToken                     ct)
    {
        var enumClassAttributeSymbol = compilation.GetTypeByMetadataName(Constants.EnumClassAttributeInfo.AttributeFullName);
        if (enumClassAttributeSymbol is null)
        {
            return null;
        }
        
        var enumInfos = new List<EnumInfo>(enums.Length);

        var enumMemberInfoAttribute = compilation.GetTypeByMetadataName(Constants.EnumMemberInfoAttributeInfo.AttributeFullName);
        
        foreach (var syntax in enums)
        {
            // Do check twice if we get cancel request in between creating EnumInfo
            // Single check might fail if 'enums' contains single element and
            // cancellation happened while creating EnumInfo
            ct.ThrowIfCancellationRequested();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
            
            // Sanity check
            if (semanticModel.GetDeclaredSymbol(syntax) is not INamedTypeSymbol
                                                               {
                                                                   EnumUnderlyingType: not null
                                                               } enumSymbol)
            {
                continue;
            }
            
            var enumInfo = EnumInfo.CreateFromNamedTypeSymbol(enumSymbol, enumClassAttributeSymbol, enumMemberInfoAttribute);
            ct.ThrowIfCancellationRequested();
            enumInfos.Add(enumInfo);
        }

        return enumInfos.Count > 0 
                   ? enumInfos 
                   : null;
    }
}