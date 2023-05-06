using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

namespace EnumClass.Generator.Benchmarks;

public class BenchmarkConfig: ManualConfig
{
    public BenchmarkConfig()
    {
        AddJob(new Job(Job.Default)
              .WithPlatform(Platform.Arm64)
              .WithPlatform(Platform.X64));
        AddDiagnoser(MemoryDiagnoser.Default);
        AddColumn(
            StatisticColumn.Min,
            StatisticColumn.Mean, 
            StatisticColumn.Max,
            StatisticColumn.StdDev);
        AddColumn();
    }
}