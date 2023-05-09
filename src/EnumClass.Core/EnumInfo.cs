using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumClass.Core;

public class EnumInfo
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

    /// <summary>
    /// Underlying type of enum - type enum inherits from
    /// </summary>
    /// <example>int, byte, ushort</example>
    public string UnderlyingType { get; }
    
    private EnumInfo(string fullyQualifiedEnumName, string className, string ns, EnumMemberInfo[] members, string underlyingType)
    {
        FullyQualifiedEnumName = fullyQualifiedEnumName;
        Namespace = ns;
        Members = members;
        UnderlyingType = underlyingType;
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
    
    public static EnumInfo? CreateFromDeclaration(EnumDeclarationSyntax syntax, 
                                                  Compilation compilation, 
                                                  INamedTypeSymbol enumClassAttribute,
                                                  INamedTypeSymbol? enumMemberInfoAttribute)
    {
        var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);
        if (ModelExtensions.GetDeclaredSymbol(semanticModel, syntax) is not INamedTypeSymbol enumSymbol)
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
                         .Select(symbol => EnumMemberInfo.CreateFromFieldSymbol(symbol, enumMemberInfoAttribute)!)
                          // And skip failed
                         .Where(i => i is not null)
                          // Finally, create array of members
                         .ToArray();
        var attributeInfo = ExtractEnumClassAttributeCtorInfo(enumSymbol, enumClassAttribute);
        var fullyQualifiedEnumName = SymbolDisplay.ToDisplayString(enumSymbol, SymbolDisplayFormat.FullyQualifiedFormat);
        var className = GetClassName(enumSymbol, attributeInfo);
        var ns = GetResultNamespace(enumSymbol, attributeInfo);
        var underlyingType = GetUnderlyingType(semanticModel);
        
        return new EnumInfo(fullyQualifiedEnumName, className, ns, memberInfos, underlyingType);
    }

    private static string GetUnderlyingType(SemanticModel semanticModel)
    {
        return "int";
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