using Ardalis.SmartEnum;

namespace EnumClass.Generator.Benchmarks.Enums;

public class PetKindSmartEnum: SmartEnum<PetKindSmartEnum>
{
    public static readonly PetKindSmartEnum Cat = new("Cat", 0);
    public static readonly PetKindSmartEnum Dog = new("Dog", 1);
    public static readonly PetKindSmartEnum Hamster = new("Hamster", 2);
    public static readonly PetKindSmartEnum Fish = new("Fish", 3);

    private PetKindSmartEnum(string name, int value) : base(name, value)
    {
        var x = ( int ) Cat;
    }
}