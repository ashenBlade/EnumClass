using System.Text;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator;

internal class EnumInfo
{
    public string ClassName { get; }
    public string FullyQualifiedEnumName { get; }
    public string Namespace { get; }
    public EnumValueInfo[] Values { get; init; }
    
    public EnumInfo(string enumName, string @namespace, EnumValueInfo[] values)
    {
        FullyQualifiedEnumName = SymbolDisplay.FormatLiteral( $"{@namespace}.{enumName}", false );
        Namespace = SymbolDisplay.FormatLiteral( @namespace, false );
        Values = values;
        ClassName = SymbolDisplay.FormatLiteral(enumName, false);
    }
    
    /// <summary>
    /// Generate code for Switch method accepting Action.
    /// This generates return type, method name, and parameters (with names)
    /// </summary>
    /// <param name="argsCount">Number of generic arguments in Action&lt;T0, T1, etc..&gt;</param>
    /// <returns>Formatted method signature</returns>
    /// <example>void Switch&lt;T0&gt;(Action&lt;CatEnumValue, T0&gt; catEnumValueSwitch, Action&lt;DogEnumValue, T0&gt; dogEnumValueSwitch)</example>
    public string GenerateSwitchActionDefinition(int argsCount)
    {
        if (TryGetSwitchActionDefinitionFromCache(argsCount, out var definition))
        {
            return definition;
        }
        
        var builder = new StringBuilder("void Switch(");
        foreach (var (value, i) in Values.Select((info, i) => (info, i)))
        {
            builder.AppendFormat("{0} {1}", value.GetActionSwitchType(0), value.GetSwitchArgName());
            if (i < Values.Length - 1)
            {
                builder.Append(", ");
            }
        }

        builder.Append(")");

        definition = builder.ToString();
        SaveSwitchActionInCache(argsCount, definition);
        return definition;
    }

    /// <summary>
    /// Generate code for Switch method accepting Func.
    ///  This generates return type, method name, and parameters (with names)
    /// </summary>
    /// <param name="argsCount">Number of arguments Func accepts as parameters. Amount without return arg</param>
    /// <returns>Formatted method signature</returns>
    /// <example>TResult Switch&lt;T0&gt;(Func&lt;CatEnumValue, TResult&gt; catEnumValueSwitch, Func&lt;DogEnumValue, TResult&gt; dogEnumValueSwitch)</example>
    public string GenerateSwitchFuncDefinition(int argsCount)
    {
        var builder = new StringBuilder("TResult Switch<TResult");
        for (int i = 0; i < argsCount; i++)
        {
            builder.AppendFormat(", T{0}", i);
        }

        builder.Append(">(");
        foreach (var (value, i) in Values.Select((info, i) => (info, i)))
        {
            builder.AppendFormat("{0} {1}", value.GetFuncSwitchType(argsCount, "TResult"), value.GetSwitchArgName());
            if (i < Values.Length - 1)
            {
                builder.Append(", ");
            }
        }

        builder.Append(")");
        return builder.ToString();
    }

    private bool TryGetSwitchActionDefinitionFromCache(int argsCount, out string value)
    {
        value = string.Empty;
        if (_actionSwitchesGeneratedCache.Count < argsCount + 1)
        {
            return false;
        }

        var saved = _actionSwitchesGeneratedCache[argsCount];
        if (saved is null)
        {
            return false;
        }

        value = saved;
        return true;
    }

    private void SaveSwitchActionInCache(int argsCount, string definition)
    {
        if (_actionSwitchesGeneratedCache.Count < argsCount + 1)
        {
            _actionSwitchesGeneratedCache.AddRange(Enumerable.Repeat<string?>(null, argsCount - _actionSwitchesGeneratedCache.Count + 1));
        }

        _actionSwitchesGeneratedCache[argsCount] = definition;
    }

    private readonly List<string?> _actionSwitchesGeneratedCache = new List<string?>(); 

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals(( EnumInfo ) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = ClassName.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ FullyQualifiedEnumName.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ Namespace.GetHashCode();
            hashCode = ( hashCode * 397 ) ^ Values.GetHashCode();
            return hashCode;
        }
    }

    public bool Equals(EnumInfo? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ClassName == other.ClassName
            && FullyQualifiedEnumName == other.FullyQualifiedEnumName
            && Namespace == other.Namespace
            && Values.Equals(other.Values);
    }
}