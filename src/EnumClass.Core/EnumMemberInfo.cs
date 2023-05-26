#nullable enable
using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Core;

public class EnumMemberInfo
{
    private readonly string _stringRepresentation;
    
    /// <summary>
    /// Friendly name of class.
    /// To use in static fields for classes 
    /// </summary>
    public string ClassName { get; }
    
    /// <summary>
    /// Class name of enum class starting with 'global::' prefix
    /// </summary>
    public string FullyQualifiedClassName { get; }
    
    /// <summary>
    /// Name of enum value we constructing.
    /// This is fully qualified, with 'global::' prefix
    /// </summary>
    public string FullyQualifiedEnumValue { get; }

    /// <summary>
    /// Name of enum member we constructing.
    /// This is without 'global::' prefix and enum name
    /// </summary>
    /// <example>Cat</example>
    public string EnumMemberNameOnly { get; }

    /// <summary>
    /// Name of enum member with original enum name prefix.
    /// </summary>
    /// <example>PetKind.Cat</example>
    public string EnumMemberNameWithEnumName { get; }
    
    /// <summary>
    /// Integer value of enum.
    /// It can be int, byte, ulong etc.
    /// Thus named integral
    /// </summary>
    public string IntegralValue { get; }

    internal EnumMemberInfo(string className,
                            string fullyQualifiedClassName,
                            string fullyQualifiedEnumValue,
                            string enumMemberNameOnly,
                            string stringRepresentation,
                            string enumMemberNameWithEnumName,
                            string integralValue)
    {
        ClassName = className;
        FullyQualifiedClassName = fullyQualifiedClassName; 
        FullyQualifiedEnumValue = fullyQualifiedEnumValue;
        EnumMemberNameOnly = enumMemberNameOnly;
        _stringRepresentation = stringRepresentation;
        EnumMemberNameWithEnumName = enumMemberNameWithEnumName;
        IntegralValue = integralValue;
    }

    /// <summary>
    /// Format string to represent switch function parameter name
    /// </summary>
    /// <returns>Argument name</returns>
    /// <example>CatEnumValue -> catEnumValueSwitch</example>
    public string GetSwitchArgName()
    {
        if (_switchArgName is not null)
        {
            return _switchArgName;
        }
        var firstLetter = char.ToLowerInvariant(EnumMemberNameOnly[0]);
        var switchArgName = $"{firstLetter}{EnumMemberNameOnly.Substring(1)}Switch";
        _switchArgName = switchArgName;
        return switchArgName;
    }

    /// <summary>
    /// Cached switch arg name
    /// </summary>
    private string? _switchArgName;

    /// <summary>
    /// Get <c>Action</c> switch type
    /// </summary>
    /// <param name="argsCount">Amount of arguments in type, including returning</param>
    /// <returns>Formatted type name</returns>
    public string GetActionSwitchType(int argsCount)
    {
        if (argsCount == 0)
        {
            return $"Action<{ClassName}>";
        }

        var builder = new StringBuilder($"Action<{ClassName}");
        for (int i = 0; i < argsCount; i++)
        {
            builder.Append(", T");
            builder.Append(i);
        }

        builder.Append(">");
        return builder.ToString();
    }

    /// <summary>
    /// Get <c>Func</c> switch type
    /// </summary>
    /// <param name="inArgsCount">Amount of arguments in type, including returning</param>
    /// <param name="resultGenericArgName">Generic name of return type</param>
    /// <returns>Formatted type name</returns>
    public string GetFuncSwitchType(int inArgsCount, string resultGenericArgName)
    {
        if (inArgsCount == 0)
        {
            return $"Func<{ClassName}, {resultGenericArgName}>";
        }

        var builder = new StringBuilder($"Func<{ClassName}");
        for (int i = 0; i < inArgsCount; i++)
        {
            builder.Append(", T");
            builder.Append(i);
        }

        builder.AppendFormat(", {0}>", resultGenericArgName);
        return builder.ToString();
    }
    
    public string GetStringRepresentationQuoted()
    {
        return SymbolDisplay.FormatLiteral(_stringRepresentation, true);
    }
}