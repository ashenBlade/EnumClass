namespace EnumClass.Generator.Tests.Integration.StringValue;

public class PunctuationMarkTests
{
    public static IEnumerable<object> PunctuationMarkWithString => new[]
    {
        new object[] {EnumClass.PunctuationMark.Dot, "."},
        new object[] {EnumClass.PunctuationMark.Comma, ","},
        new object[] {EnumClass.PunctuationMark.Exclamation, "!"},
        new object[] {EnumClass.PunctuationMark.Question, "?"},
    };

    [Theory]
    [MemberData(nameof(PunctuationMarkWithString))]
    public void ToString__WithStringValueAttribute__ShouldReturnSpecifiedValue(EnumClass.PunctuationMark mark, string expected)
    {
        var actual = mark.ToString();
        Assert.Equal(expected, actual);
    }
}