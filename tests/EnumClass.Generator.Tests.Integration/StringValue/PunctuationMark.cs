using EnumClass.Attributes;

namespace EnumClass.Generator.Tests.Integration.StringValue;

[EnumClass]
public enum PunctuationMark
{
    [StringValue(".")]
    Dot,
    [StringValue(",")]
    Comma,
    [StringValue("!")]
    Exclamation,
    [StringValue("?")]
    Question,
}