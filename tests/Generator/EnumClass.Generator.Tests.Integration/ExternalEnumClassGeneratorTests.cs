using EnumClass.Attributes;
using SampleEnums;

[assembly: ExternalEnumClass(typeof(Token))]
namespace EnumClass.Generator.Tests.Integration;

public class ExternalEnumClassGeneratorTests
{
    public static SampleEnums.EnumClass.Token Keyword => SampleEnums.EnumClass.Token.Keyword;
    public static SampleEnums.EnumClass.Token Identifier => SampleEnums.EnumClass.Token.Identifier;
    public static SampleEnums.EnumClass.Token Word => SampleEnums.EnumClass.Token.Word;
    public static SampleEnums.EnumClass.Token Trivia => SampleEnums.EnumClass.Token.Trivia;
    
    [Fact]
    public void ToString_WithGeneratedClass_ShouldReturnMemberNames()
    {
        Assert.Equal("Keyword", Keyword.ToString());
        Assert.Equal("Identifier", Identifier.ToString());
        Assert.Equal("Word", Word.ToString());
        Assert.Equal("Trivia", Trivia.ToString());
    }

    [Fact]
    public void EqualMembers__ShouldReturnTrueOnComparisonWithOriginalEnum()
    {
        Assert.True(Keyword == Token.Keyword);
        Assert.True(Word == Token.Word);
        Assert.True(Trivia == Token.Trivia);
        Assert.True(Identifier == Token.Identifier);
    }
}