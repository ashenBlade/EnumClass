using Microsoft.CodeAnalysis;

namespace EnumClass.Generator.Infrastructure;

public static class SeparatedSyntaxListExtensions
{
    public static bool Any<T>(this SeparatedSyntaxList<T> list, Func<T, bool> filter) where T : SyntaxNode
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (filter(list[i]))
            {
                return true;
            }
        }

        return false;
    }
    
    public static T? FirstOrDefault<T>(this SeparatedSyntaxList<T> list, Func<T, bool> filter) where T : SyntaxNode
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (filter(list[i]))
            {
                return list[i];
            }
        }

        return null;
    }
}