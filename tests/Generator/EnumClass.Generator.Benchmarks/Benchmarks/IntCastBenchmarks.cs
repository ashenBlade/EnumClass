using BenchmarkDotNet.Attributes;

namespace EnumClass.Generator.Benchmarks.Benchmarks;

public class IntCastBenchmarks: BaseBenchmark
{
    [Benchmark]
    public int CastToInt_Enum() => (int) RawEnumValue;
    
    [Benchmark]
    public int CastToInt_EnumClass() => (int) EnumClassValue;

    [Benchmark]
    public int CastToInt_SmartEnum() => (int) SmartEnumValue;
}