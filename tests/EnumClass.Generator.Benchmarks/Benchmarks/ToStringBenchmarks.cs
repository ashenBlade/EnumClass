using BenchmarkDotNet.Attributes;

namespace EnumClass.Generator.Benchmarks.Benchmarks;

public class ToStringBenchmarks: BaseBenchmark
{
    [Benchmark]
    public string ToString_Enum_Cat() => RawEnumValue.ToString();
    
    [Benchmark]
    public string ToString_SmartEnum_Cat() => SmartEnumValue.ToString();

    [Benchmark]
    public string ToString_EnumClass_Cat() => EnumClassValue.ToString()!;
}