using EnumClass.Attributes;

namespace EnumClass.Generator.Tests.Integration;

public class ToStringTests
{
    [EnumClass]
    public enum PunctuationMark
    {
        [EnumMemberInfo(StringValue = ".")]
        Dot,
        [EnumMemberInfo(StringValue = ",")]
        Comma,
        [EnumMemberInfo(StringValue = "!")]
        Exclamation,
        [EnumMemberInfo(StringValue = "?")]
        Question,
    }
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
    
    [EnumClass]
    public enum NumbersEnum
    {
        One,
        Two,
        Three,
        Four,
        Six
    }
    
    public static IEnumerable<IEnumerable<object>> NumbersEnumClassWithString = new[]
    {
        new object[] {EnumClass.NumbersEnum.One, "One"}, new object[] {EnumClass.NumbersEnum.Two, "Two"},
        new object[] {EnumClass.NumbersEnum.Three, "Three"}, new object[] {EnumClass.NumbersEnum.Four, "Four"},
        new object[] {EnumClass.NumbersEnum.Six, "Six"},
    };
    
    [Theory]
    [MemberData(nameof(NumbersEnumClassWithString))]
    public void ToString__WithoutOverridenDefault__ShouldReturnEnumMemberNameString(
        EnumClass.NumbersEnum enumClass,
        string expected)
    {
        var actual = enumClass.ToString();
        Assert.Equal(expected, actual);
    }
}