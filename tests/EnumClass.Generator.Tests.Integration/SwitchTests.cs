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
}