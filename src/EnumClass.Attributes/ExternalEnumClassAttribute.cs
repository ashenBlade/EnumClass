using System;

namespace EnumClass.Attributes;

/// <summary>
/// Marker attribute for EnumClassGenerator
/// used for generating EnumClass for enums in external assemblies
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class ExternalEnumClassAttribute: Attribute
{
    /// <summary>
    /// Primary constructor
    /// </summary>
    /// <param name="enum">Type of enum for generating</param>
    public ExternalEnumClassAttribute(Type @enum)
    { }
    
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