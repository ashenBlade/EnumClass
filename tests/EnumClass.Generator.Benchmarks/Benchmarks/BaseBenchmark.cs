using BenchmarkDotNet.Attributes;
using EnumClass.Generator.Benchmarks.Enums;

namespace EnumClass.Generator.Benchmarks.Benchmarks;

[Config(typeof(BenchmarkConfig))]
public class BaseBenchmark
{
    public static (PetKind EnumValue, EnumClassPetKind.PetKind, PetKindSmartEnum SmartEnumValue)[]
        _values { get; } = {
            ( PetKind.Cat, EnumClassPetKind.PetKind.Cat, PetKindSmartEnum.Cat ),
            ( PetKind.Dog, EnumClassPetKind.PetKind.Dog, PetKindSmartEnum.Dog ),
            ( PetKind.Hamster, EnumClassPetKind.PetKind.Hamster, PetKindSmartEnum.Hamster ),
            ( PetKind.Fish, EnumClassPetKind.PetKind.Fish, PetKindSmartEnum.Fish ),
        };
    
    [ParamsSource(nameof(_values))]
    public (PetKind EnumValue, EnumClassPetKind.PetKind EnumClassValue, PetKindSmartEnum SmartEnumValue) Values { get; set; }


    public PetKind RawEnumValue => Values.EnumValue;
    public EnumClassPetKind.PetKind EnumClassValue => Values.EnumClassValue;
    public PetKindSmartEnum SmartEnumValue => Values.SmartEnumValue;
}