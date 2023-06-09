using EnumClass.Attributes;

namespace EnumClass.Generator.Tests.Integration;

[EnumClass]
public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}

public class ComparisonTests
{
    public static readonly EnumClass.LogLevel Debug = EnumClass.LogLevel.Debug;
    public static readonly EnumClass.LogLevel Info = EnumClass.LogLevel.Info;
    public static readonly EnumClass.LogLevel Warning = EnumClass.LogLevel.Warning;
    public static readonly EnumClass.LogLevel Error = EnumClass.LogLevel.Error;
    public static readonly EnumClass.LogLevel Fatal = EnumClass.LogLevel.Fatal;
    public static IEnumerable<object[]> SameEnumClassMembers => EnumClass.LogLevel
                                                                    .GetAllMembers()
                                                                    .Select(x => new object[]{x});
    [Theory]
    [MemberData(nameof(SameEnumClassMembers))]
    public void EqualityOperator__WithSameEnumClass__ShouldReturnTrue(EnumClass.LogLevel level)
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.True(level == level);
    }
    
    [Theory]
    [MemberData(nameof(SameEnumClassMembers))]
    public void Equals__WithSameEnumClass__ShouldReturnTrue(EnumClass.LogLevel level)
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.True(level.Equals(level));
    }

    public static IEnumerable<object[]> EnumClassWithCorresponding => new[]
    {
        new object[] { Debug, LogLevel.Debug },
        new object[] { Info, LogLevel.Info },
        new object[] { Warning, LogLevel.Warning },
        new object[] { Error, LogLevel.Error },
        new object[] { Fatal, LogLevel.Fatal },
    };
    
    [Theory]
    [MemberData(nameof(EnumClassWithCorresponding))]
    public void EqualityOperator__WithCorrespondingEnum__ShouldReturnTrue(
        EnumClass.LogLevel level,
        LogLevel corresponding)
    {
        Assert.True(level == corresponding);
    }

    [Theory]
    [MemberData(nameof(SameEnumClassMembers))]
    public void EqualityOperator__WithNull__ShouldReturnFalse(EnumClass.LogLevel level)
    {
        Assert.False(level == null!);
        Assert.False(null! == level!);
    }
    
    [Theory]
    [MemberData(nameof(SameEnumClassMembers))]
    public void NeEqualityOperator__WithNull__ShouldReturnTrue(EnumClass.LogLevel level)
    {
        Assert.True(level != null!);
        Assert.True(null! != level!);
    }
    
    [Theory]
    [MemberData(nameof(SameEnumClassMembers))]
    public void Equals__WithNull__ShouldReturnFalse(EnumClass.LogLevel level)
    {
        Assert.False(level.Equals(null));
    }

    public static IEnumerable<object[]> EnumClassWithDifferent => EnumClass.LogLevel
                                                                           .GetAllMembers()
                                                                           .ToArray()
                                                                           .GetDifferent()
                                                                           .Select(x => new object[]{x.Value, x.Others} );

    [Theory]
    [MemberData(nameof(EnumClassWithDifferent))]
    public void EqualityOperator__WithDifferentEnumMembers__ShouldReturnFalse(
        EnumClass.LogLevel value,
        EnumClass.LogLevel[] rest)
    {
        foreach (var l in rest)
        {
            Assert.False(l == value);
            Assert.False(value == l);
        }
    }
    
    [Theory]
    [MemberData(nameof(EnumClassWithDifferent))]
    public void NeEqualityOperator__WithDifferentEnumMembers__ShouldReturnTrue(
        EnumClass.LogLevel value,
        EnumClass.LogLevel[] rest)
    {
        foreach (var l in rest)
        {
            Assert.True(l != value);
            Assert.True(value != l);
        }
    }
    
    [Theory]
    [MemberData(nameof(EnumClassWithDifferent))]
    public void Equals__WithDifferentEnumMembers__ShouldReturnFalse(
        EnumClass.LogLevel value,
        EnumClass.LogLevel[] rest)
    {
        foreach (var l in rest)
        {
            Assert.False(value.Equals(l));
        }
    }

    public static IEnumerable<object[]> EnumClassWithDifferentEnums => EnumClass.LogLevel
                                                                                .GetAllMembers()
                                                                                .ToArray()
                                                                                .GetDifferent()
                                                                                .Select(x => new object[]{x.Value, x.Others.Select(v => (LogLevel)v)} );

    [Theory]
    [MemberData(nameof(EnumClassWithDifferentEnums))]
    public void EqualityOperator__WithDifferentRawEnums__ShouldReturnFalse(
        EnumClass.LogLevel value,
        IEnumerable<LogLevel> rest)
    {
        foreach (var l in rest)
        {
            Assert.False(l == value);
            Assert.False(value == l);
        }
    }
    
    [Theory]
    [MemberData(nameof(EnumClassWithDifferentEnums))]
    public void NeEqualityOperator__WithDifferentRawEnums__ShouldReturnTrue(
        EnumClass.LogLevel value,
        IEnumerable<LogLevel> rest)
    {
        foreach (var l in rest)
        {
            Assert.True(l != value);
            Assert.True(value != l);
        }
    }
    
    [Theory]
    [MemberData(nameof(EnumClassWithDifferentEnums))]
    public void Equals__WithDifferentRawEnums__ShouldReturnFalse(
        EnumClass.LogLevel value,
        IEnumerable<LogLevel> rest)
    {
        foreach (var l in rest)
        {
            Assert.False(value.Equals(l));
        }
    }
}