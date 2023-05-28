using EnumClass.Attributes;

namespace EnumClass.Generator.Tests.Integration;

public class TryParseTests
{
    [EnumClass]
    public enum OrdinalNumberEnumeration
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
    }
    
    public static IEnumerable<IEnumerable<object>> ExpectedAndEnumMember => new[]
    {
        new object[] {EnumClass.OrdinalNumberEnumeration.First, "First"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Second, "Second"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Third, "Third"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Fourth, "Fourth"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Fifth, "Fifth"},
    };
    
    [Theory]
    [MemberData(nameof(ExpectedAndEnumMember))]
    public void WhenStringEqualsToOriginalMember__ShouldReturnTrueAndReturnCorrectInstance(
        EnumClass.OrdinalNumberEnumeration expected,
        string value)
    {
        if (!EnumClass.OrdinalNumberEnumeration.TryParse(value, out var actual))
        {
            Assert.True(false, "When parsed correctly should return true");
        }
        Assert.Equal(expected, actual);
    }
    
    public static IEnumerable<IEnumerable<object>> ExpectedAndFullEnumName => new[]
    {
        new object[] {EnumClass.OrdinalNumberEnumeration.First, "OrdinalNumberEnumeration.First"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Second, "OrdinalNumberEnumeration.Second"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Third, "OrdinalNumberEnumeration.Third"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Fourth, "OrdinalNumberEnumeration.Fourth"},
        new object[] {EnumClass.OrdinalNumberEnumeration.Fifth, "OrdinalNumberEnumeration.Fifth"},
    };
    
    
    [Theory]
    [MemberData(nameof(ExpectedAndFullEnumName))]
    public void Action_WithPrerequisites_ShouldBehaviour(EnumClass.OrdinalNumberEnumeration expected, string value)
    {
        
        if (!EnumClass.OrdinalNumberEnumeration.TryParse(value, out var actual))
        {
            Assert.True(false, "When parsed correctly should return true");
        }
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void WhenPassedNull__ShouldReturnFalse()
    {
        var actual = EnumClass.OrdinalNumberEnumeration.TryParse(null!, out _);
        Assert.False(actual);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("null")]
    [InlineData("Test")]
    [InlineData("Fist")]
    [InlineData("Secod")]
    [InlineData("OrdinalNumberEnumeration.Fist")]
    public void WhenPassedIncorrectString__ShouldReturnFalse(string value)
    {
        var actual = EnumClass.OrdinalNumberEnumeration.TryParse(value, out _);
        Assert.False(actual);
    }
}