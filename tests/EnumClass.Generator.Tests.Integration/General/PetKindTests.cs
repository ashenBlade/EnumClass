namespace EnumClass.Generator.Tests.Integration.General;

public class PetKindTests
{
    public static readonly IEnumerable<object> AppropriateMembers = new[]
    {
        new object[] {EnumClass.PetKind.Cat, PetKind.Cat}, 
        new object[] {EnumClass.PetKind.Dog, PetKind.Dog},
        new object[] {EnumClass.PetKind.Hamster, PetKind.Hamster}, 
        new object[] {EnumClass.PetKind.Fish, PetKind.Fish},
        new object[] {EnumClass.PetKind.Parrot, PetKind.Parrot},
    };

    [Theory]
    [MemberData(nameof(AppropriateMembers))]
    public void Equals__WithAppropriateMembers__ShouldReturnTrue(EnumClass.PetKind enumClass, PetKind originalEnum)
    {
        Assert.True(enumClass.Equals(originalEnum), "enumClass.Equals(originalEnum)");
        Assert.True(enumClass == originalEnum, "enumClass == originalEnum");
    }


    [Theory]
    [MemberData(nameof(AppropriateMembers))]
    public void GetHashCode__ShouldReturnSameAsOriginalEnum(EnumClass.PetKind enumClass, PetKind originalEnum)
    {
        Assert.True(enumClass.GetHashCode() == originalEnum.GetHashCode(), "enumClass.GetHashCode() == originalEnum.GetHashCode()");
    }

    [Theory]
    [MemberData(nameof(AppropriateMembers))]
    public void CastToOriginalEnum__ShouldReturnAppropriateOriginalEnum(EnumClass.PetKind enumClass, PetKind originalEnum)
    {
        var actual = ( PetKind ) enumClass;
        Assert.Equal(originalEnum, actual);
    }

    public static readonly IEnumerable<object> EnumClassWithToString = new object[]
    {
        new object[] {EnumClass.PetKind.Cat, "Cat"},
        new object[] {EnumClass.PetKind.Dog, "Dog"},
        new object[] {EnumClass.PetKind.Hamster, "Hamster"},
        new object[] {EnumClass.PetKind.Fish, "Fish"},
        new object[] {EnumClass.PetKind.Parrot, "Parrot"},
    };

    [Theory]
    [MemberData(nameof(EnumClassWithToString))]
    public void ToString__WithoutOverridenDefaultValues__ShouldReturnOriginalEnumNames(EnumClass.PetKind enumClass, string expected)
    {
        var actual = enumClass.ToString();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SwitchFunc_WithCatEnumValue_ShouldCallCatHandler()
    {
        var expected = true;
        var nonExpected = false;
        var actual = EnumClass.PetKind.Cat.Switch(
            cat => expected,
            dog => nonExpected,
            hamster => nonExpected,
            parrot => nonExpected,
            fish => nonExpected);
        Assert.True(actual, "Switch should call cat handler");
    }
    
    [Fact]
    public void SwitchFunc_WithHamsterEnumValue_ShouldCallHamsterHandler()
    {
        var expected = true;
        var nonExpected = false;
        var actual = EnumClass.PetKind.Hamster.Switch(
            cat => nonExpected,
            dog => nonExpected,
            hamster => expected,
            parrot => nonExpected,
            fish => nonExpected);
        Assert.True(actual, "Switch should call hamster handler");
    }
}