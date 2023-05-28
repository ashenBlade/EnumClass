using BenchmarkDotNet.Attributes;

namespace EnumClass.Generator.Benchmarks.Benchmarks;

public class GetHashCodeBenchmarks: BaseBenchmark
{
    [Benchmark]
    public int GetHashCode_Enum() => RawEnumValue.GetHashCode();

    [Benchmark]
    public int GetHashCode_EnumClass() => EnumClassValue.GetHashCode();

    [Benchmark]
    public int GetHashCode_SmartEnum() => SmartEnumValue.GetHashCode();
}