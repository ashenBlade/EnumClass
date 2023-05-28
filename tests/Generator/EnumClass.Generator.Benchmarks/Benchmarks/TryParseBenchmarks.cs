using BenchmarkDotNet.Attributes;
using EnumClass.Generator.Benchmarks.Enums;

namespace EnumClass.Generator.Benchmarks.Benchmarks;

public class TryParseBenchmarks
{
    [Config(typeof(BenchmarkConfig))]
    public class ValidValues
    {
        [Params("Dog", "PetKind.Dog",
            "Cat", "PetKind.Cat",
            "Hamster", "PetKind.Hamster",
            "Fish", "PetKind.Fish")]
        public string ValidStringValue { get; set; } = null!;

        [Benchmark]
        public bool TryParse_RawEnum_Valid() => Enum.TryParse<PetKind>(ValidStringValue, out _);

        [Benchmark]
        public bool TryParse_SmartEnum_Valid() => PetKindSmartEnum.TryFromName(ValidStringValue, out _);

        [Benchmark]
        public bool TryParse_EnumClass_Valid() => EnumClassPetKind.PetKind.TryParse(ValidStringValue, out _);
    }
    
    [Config(typeof(BenchmarkConfig))]
    public class InvalidValues
    {
        public const string InvalidValue = "SomeInvalidValue";
        [Benchmark]
        public bool TryParse_RawEnum_Valid() => Enum.TryParse<PetKind>(InvalidValue, out _);

        [Benchmark]
        public bool TryParse_SmartEnum_Valid() => PetKindSmartEnum.TryFromName(InvalidValue, out _);

        [Benchmark]
        public bool TryParse_EnumClass_Valid() => EnumClassPetKind.PetKind.TryParse(InvalidValue, out _);
    }
}