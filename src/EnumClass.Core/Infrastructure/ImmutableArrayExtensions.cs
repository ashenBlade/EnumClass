using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Core.Infrastructure;

internal static class ImmutableArrayExtensions
{
    // ReSharper disable once LoopCanBeConvertedToQuery
    public static IEnumerable<(T1 Left, T2 Right)> Combine<T1, T2>(this IEnumerable<T1> array, T2 value)
    {
        foreach (var element in array)
        {
            yield return ( element, value );
        }
    }
}