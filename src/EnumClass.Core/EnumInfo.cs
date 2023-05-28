#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnumClass.Core.Accessibility;
using EnumClass.Core.SymbolName;
using EnumClass.Core.UnderlyingType;

namespace EnumClass.Core;

public class EnumInfo
{
    private readonly ISymbolName _className;
    private readonly ISymbolName _enumName;
    private readonly ISymbolName _namespace;

    /// <summary>
    /// Only class name without namespace
    /// </summary>
    public string ClassName => _className.Plain;

    /// <summary>
    /// Fully qualified name of generated class
    /// </summary>
    public string FullyQualifiedClassName => _className.FullyQualified;
    
    /// <summary>
    /// Fully qualified name of original enum
    /// </summary>
    public string FullyQualifiedEnumName => _enumName.FullyQualified;

    /// <summary>
    /// Namespace of generated class
    /// </summary>
    public string Namespace => _namespace.Plain;
    
    /// <summary>
    /// Members of enum
    /// </summary>
    public EnumMemberInfo[] Members { get; }

    /// <summary>
    /// Underlying type of enum - type enum inherits from
    /// </summary>
    /// <example>byte, sbyte, short, ushort, int, uint, long, ulong</example>
    public IUnderlyingType UnderlyingType { get; }

    /// <summary>
    /// Accessibility of original enum
    /// </summary>
    public IAccessibility Accessibility { get; }
    
    internal EnumInfo(ISymbolName className,
                      ISymbolName enumName,
                      EnumMemberInfo[] members,
                      IUnderlyingType underlyingType,
                      IAccessibility accessibility,
                      ISymbolName @namespace)
    {
        _className = className;
        _enumName = enumName;
        Members = members;
        UnderlyingType = underlyingType;
        Accessibility = accessibility;
        _namespace = @namespace;
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
        
        var builder = new StringBuilder("void Switch");
        // Generate action that accepts arguments
        if (argsCount > 0)
        {
            // Generate method generic arguments
            builder.Append("<");
            for (int i = 0; i < argsCount; i++)
            {
                builder.AppendFormat("T{0}", i);
                if (i < argsCount - 1)
                {
                    builder.Append(", ");
                }
            }

            // Generate input parameters
            builder.Append(">(");
            for (var i = 0; i < argsCount; i++)
            {
                builder.AppendFormat("T{0} arg{0}", i);
                if (i < argsCount)
                {
                    builder.Append(", ");
                }
            }
        }
        else
        {
            builder.Append("(");
        }

        // Generate switch arguments
        foreach (var (value, i) in Members.Select((info, i) => (info, i)))
        {
            builder.AppendFormat("{0} {1}", value.GetActionSwitchType(argsCount), value.GetSwitchArgName());
            if (i < Members.Length - 1)
            {
                builder.Append(", ");
            }
        }

        builder.Append(")");
        // return builder.ToString();
        definition = builder.ToString();
        SaveSwitchActionInCache(argsCount, definition);
        return definition;
    }

    /// <summary>
    /// Generate code for Switch method accepting Func.
    /// This generates return type, method name, and parameters (with names)
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
        if (argsCount > 0)
        {
            for (var i = 0; i < argsCount; i++)
            {
                builder.AppendFormat("T{0} arg{0}, ", i);
            }
        }
        foreach (var (value, i) in Members.Select((info, i) => (info, i)))
        {
            builder.AppendFormat("{0} {1}", value.GetFuncSwitchType(argsCount, "TResult"), value.GetSwitchArgName());
            if (i < Members.Length - 1)
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

    private readonly List<string?> _actionSwitchesGeneratedCache = new();

    private string? _cachedVariableNameNoSuffix;
    
    public string GetVariableName(string? suffix = null)
    {
        return suffix is null
                   ? GetCachedVarName()
                   : GetCachedVarName() + suffix;
        
        string GetCachedVarName()
        {
            return _cachedVariableNameNoSuffix ??= char.ToLower(ClassName[0]) + ClassName.Substring(1);
        }
    }
}