using System;

namespace EnumClass.Attributes;

[AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
public class EnumClassAttribute : Attribute
{
    /// <summary>
    /// Namespace where generated class will be contained.
    /// Defaults to namespace of original enum + "".EnumClass""
    /// </summary>
    public string Namespace { get; set; } = null!;

    /// <summary>
    /// Name of class that will be generated.
    /// Defaults to the same name of enum
    /// </summary>
    public string ClassName { get; set; } = null!;
}