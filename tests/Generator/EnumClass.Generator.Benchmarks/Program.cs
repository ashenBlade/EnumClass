using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Running;
using EnumClass.Generator.Benchmarks.Benchmarks;
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static")]

BenchmarkRunner.Run(new[]
{
    typeof(ToStringBenchmarks),
    typeof(EqualsBenchmarks),
    typeof(IntCastBenchmarks),
    typeof(GetHashCodeBenchmarks),
    typeof(TryParseBenchmarks.InvalidValues),
    typeof(TryParseBenchmarks.ValidValues),
});