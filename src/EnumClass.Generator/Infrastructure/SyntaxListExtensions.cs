using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Generator.Infrastructure;

public static class SyntaxListExtensions
{
    // ReSharper disable once ForCanBeConvertedToForeach
    // ReSharper disable once LoopCanBeConvertedToQuery
    public static bool Any(this SyntaxList<AttributeListSyntax> list, Func<AttributeSyntax, bool> predicate)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var attributes = list[i].Attributes;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var j = 0; j < attributes.Count; j++)
            {
                if (predicate(attributes[j]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}