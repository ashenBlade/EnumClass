using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Generator;

internal class EnumInfo
{
    /// <summary>
    /// Only class name without namespace
    /// </summary>
    public string ClassName { get; }
    /// <summary>
    /// Fully qualified name of original enum
    /// </summary>
    public string FullyQualifiedEnumName { get; }
    /// <summary>
    /// Namespace of original enum
    /// </summary>
    public string Namespace { get; }
    /// <summary>
    /// Members of enum
    /// </summary>
    public EnumMemberInfo[] Members { get; }
    
    private EnumInfo(string fullyQualifiedEnumName, string className, string ns, EnumMemberInfo[] members)
    {
        FullyQualifiedEnumName = fullyQualifiedEnumName;
        Namespace = ns;
        Members = members;
        ClassName = className;
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

    private readonly List<string?> _actionSwitchesGeneratedCache = new List<string?>(); 
    
    public static EnumInfo? CreateFromDeclaration(EnumDeclarationSyntax syntax, Compilation compilation, INamedTypeSymbol? stringValueAttribute)
    {
        var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
        if (semanticModel.GetDeclaredSymbol(syntax) is not { } enumSymbol)
        {
            return null;
        }

        var members = enumSymbol.GetMembers();
        
        var memberInfos = members
                          // Skip all non enum fields declarations
                         .OfType<IFieldSymbol>()
                          // Enum members are all const, according to docs
                         .Where(m => m is {IsConst: true, HasConstantValue:true})
                          // Try to convert them into EnumMemberInfo
                         .Select(symbol => EnumMemberInfo.CreateFromFieldSymbol(symbol, stringValueAttribute)!)
                          // And skip failed
                         .Where(valueInfo => valueInfo is not null)
                          // Finally, create array of members
                         .ToArray();

        var fullyQualifiedEnumName = SymbolDisplay.ToDisplayString(enumSymbol, SymbolDisplayFormat.FullyQualifiedFormat);
        var className = SymbolDisplay.FormatLiteral(enumSymbol.Name, false);
        var ns = enumSymbol.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        
        return new EnumInfo(fullyQualifiedEnumName, className, ns, memberInfos);
    }
}