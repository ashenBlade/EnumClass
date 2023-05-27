using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using EnumClass.Generator.Benchmarks.Enums;

namespace EnumClass.Generator.Benchmarks.Benchmarks;

public class EqualsBenchmarks: BaseBenchmark
{
    [Benchmark]
    public bool Equals_Enum_Cat() => RawEnumValue.Equals(PetKind.Cat);
    
    [Benchmark]
    public bool Equals_SmartEnum_Cat() => SmartEnumValue.Equals(PetKindSmartEnum.Cat);
    
    // ReSharper disable once SuspiciousTypeConversion.Global
    [Benchmark]
    public bool Equals_SmartEnum_RawEnumCat() => SmartEnumValue.Equals(PetKind.Cat);
    
    [Benchmark]
    public bool Equals_EnumClass_Cat() => EnumClassValue.Equals(EnumClassPetKind.PetKind.Cat);
    
    [Benchmark]
    public bool Equals_EnumClass_RawEnumCat() => EnumClassValue.Equals(PetKind.Cat);
}