using System.Diagnostics.CodeAnalysis;
using EnumClass.Attributes;


namespace EnumClass.Generator.Tests.Integration;

[EnumClass]
public enum RoomState
{
    Free,
    Busy,
    Rent,
    Cleaning
}

// ReSharper disable once UnusedParameter.Local
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public class SwitchTests
{
    [Fact]
    public void ActionSwitch__WithFreeState__ShouldCallFreeHandler()
    {
        var free = EnumClass.RoomState.Free;
        var called = false;
        free.Switch(
            freeState => called = true,
            _ => Assert.True(false),
            _ => Assert.True(false),
            _ => Assert.True(false));
        
        Assert.True(called);
    }

    [Fact]
    public void ActionSwitch__WithPassedValue__ShouldPassExactValueToHandlers()
    {
        var free = EnumClass.RoomState.Free;
        var expected = 42;
        free.Switch(expected,
            (freeState, i) => Assert.Equal(expected, i),
            (_, i) => Assert.True(false, "Should not be called"),
            (_, i) => Assert.True(false, "Should not be called"),
            (_, i) => Assert.True(false, "Should not be called"));
    }

    [Fact]
    public void FuncSwitch__WithCalculatedValue__ShouldReturnSpecifiedValue()
    {
        var state = EnumClass.RoomState.Free;
        var expected = 42;
        var actual = state.Switch(
            free => expected,
            _ => expected + 1,
            _ => expected + 2,
            _ => expected + 3);
        Assert.Equal(expected, actual);
    }
}