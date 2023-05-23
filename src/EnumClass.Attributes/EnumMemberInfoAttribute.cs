using System;

namespace EnumClass.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class EnumMemberInfoAttribute: Attribute
{
    /// <summary>
    /// String representation of enum (returned by <c>ToString()</c>).
    /// By defaults to enum member name without enum class name
    /// </summary>
    /// <remarks>
    /// <c>null</c> and empty strings are discarded  
    /// </remarks>
    public string StringValue { get; set; } = null!;
}