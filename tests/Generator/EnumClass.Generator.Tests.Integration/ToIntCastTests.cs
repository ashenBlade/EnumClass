using EnumClass.Attributes;

namespace EnumClass.Generator.Tests.Integration;

public class ToIntCastTests
{
    [EnumClass]
    public enum SequentialEnum
    {
        First,
        Second,
        Third,
        Fourth
    }
    
    
    [EnumClass]
    public enum PartiallySequentialEnum
    {
        First,
        Second,
        Third = 50,
        Fourth = -100
    }
    
    
    [EnumClass]
    public enum NonSequentialEnum
    {
        First = 4,
        Second = 3,
        Third = 2,
        Fourth = 1,
    }
    
    public static readonly IEnumerable<IEnumerable<object>> NonSequentialEnumWithClass = new[]
    {
        new object[] {NonSequentialEnum.First, EnumClass.NonSequentialEnum.First},
        new object[] {NonSequentialEnum.Second, EnumClass.NonSequentialEnum.Second},
        new object[] {NonSequentialEnum.Third, EnumClass.NonSequentialEnum.Third},
        new object[] {NonSequentialEnum.Fourth, EnumClass.NonSequentialEnum.Fourth},
    };
    
    [Theory]
    [MemberData(nameof(NonSequentialEnumWithClass))]
    public void CastToInt_WithCustomOrdinalNumbers_ShouldReturnIntAsInOriginalEnum(NonSequentialEnum originalEnum, EnumClass.NonSequentialEnum enumClass)
    {
        var expected = ( int ) originalEnum;
        var actual = (int) enumClass;
        Assert.Equal(expected, actual);
    }
    
    public static readonly IEnumerable<IEnumerable<object>> SequentialEnumWithClass = new[]
    {
        new object[] {SequentialEnum.First, EnumClass.SequentialEnum.First},
        new object[] {SequentialEnum.Second, EnumClass.SequentialEnum.Second},
        new object[] {SequentialEnum.Third, EnumClass.SequentialEnum.Third},
        new object[] {SequentialEnum.Fourth, EnumClass.SequentialEnum.Fourth},
    };
    
    [Theory]
    [MemberData(nameof(SequentialEnumWithClass))]
    public void CastToInt_WithNotChangedNumbers_ShouldReturnIntAsInOriginalEnum(SequentialEnum originalEnum, EnumClass.SequentialEnum enumClass)
    {
        var expected = ( int ) originalEnum;
        var actual = (int) enumClass;
        Assert.Equal(expected, actual);
    }
    
    public static readonly IEnumerable<IEnumerable<object>> PartiallySequentialEnumWithClass = new[]
    {
        new object[] {PartiallySequentialEnum.First, EnumClass.PartiallySequentialEnum.First},
        new object[] {PartiallySequentialEnum.Second, EnumClass.PartiallySequentialEnum.Second},
        new object[] {PartiallySequentialEnum.Third, EnumClass.PartiallySequentialEnum.Third},
        new object[] {PartiallySequentialEnum.Fourth, EnumClass.PartiallySequentialEnum.Fourth},
    };
    
    [Theory]
    [MemberData(nameof(PartiallySequentialEnumWithClass))]
    public void CastToInt_WithPartiallyChangedNumbers_ShouldReturnIntAsInOriginalEnum(PartiallySequentialEnum originalEnum, EnumClass.PartiallySequentialEnum enumClass)
    {
        var expected = ( int ) originalEnum;
        var actual = (int) enumClass;
        Assert.Equal(expected, actual);
    }
}