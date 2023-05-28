using EnumClass.Attributes;

namespace EnumClass.Generator.Tests.Integration;

public class GeneralObjectMethodsTests
{
    [EnumClass]
    public enum PetKind
    {
        Cat,
        Dog,
        Hamster,
        Parrot,
        Fish,
    }
    
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

    public static readonly IEnumerable<IEnumerable<object>> DifferentEnumClassPairs = new[]
    {
        new object[] {EnumClass.PetKind.Cat, EnumClass.PetKind.Dog},
        new object[] {EnumClass.PetKind.Cat, EnumClass.PetKind.Fish},
        new object[] {EnumClass.PetKind.Cat, EnumClass.PetKind.Hamster},
        new object[] {EnumClass.PetKind.Cat, EnumClass.PetKind.Parrot},
        new object[] {EnumClass.PetKind.Dog, EnumClass.PetKind.Parrot},
        new object[] {EnumClass.PetKind.Dog, EnumClass.PetKind.Hamster},
        new object[] {EnumClass.PetKind.Dog, EnumClass.PetKind.Fish},
        new object[] {EnumClass.PetKind.Parrot, EnumClass.PetKind.Hamster},
    };

    [Theory]
    [MemberData(nameof(DifferentEnumClassPairs))]
    public void Equals__WithDifferentMembers__ShouldReturnFalse(EnumClass.PetKind left, EnumClass.PetKind right)
    {
        Assert.NotEqual(left, right);
    }
    
    [Theory]
    [MemberData(nameof(AppropriateMembers))]
    public void GetHashCode__ShouldReturnSameAsOriginalEnum(EnumClass.PetKind enumClass, PetKind originalEnum)
    {
        Assert.True(enumClass.GetHashCode() == originalEnum.GetHashCode(), "enumClass.GetHashCode() == originalEnum.GetHashCode()");
    }
}