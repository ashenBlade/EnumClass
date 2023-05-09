using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace EnumClass.Generator.Tests;

/// <summary>
/// Tests for checking that target classes are actually generated
/// </summary>
public class EnumClassGenerationTests
{
    [Fact]
    public void WhenEnumHasEnumClassAttribute__EnumClassShouldBeGenerated()
    {
        var (compilation, _) = TestHelper.CompileDll(@"using EnumClass.Attributes;

namespace Test;

[EnumClass]
public enum SampleEnum
{
    First,
    Second,
    Third
}");
        // By default all generated with suffix "EnumClass"
        var targetEnumClassName = "Test.EnumClass.SampleEnum";
        var typeSymbol = compilation.GetTypeByMetadataName(targetEnumClassName);
        Assert.NotNull(typeSymbol);
    }

    [Fact]
    public void WhenEnumDoNotHasEnumClassAttribute__EnumClassShouldNotBeGenerated()
    {
        var (compilation, _) = TestHelper.CompileDll( @"using EnumClass.Attributes;
namespace Test;

public enum SampleEnum
{
    First,
    Second,
    Third
}" );
        // By default all generated with suffix "EnumClass"
        var targetEnumClassName = "Test.EnumClass.SampleEnum";
        var typeSymbol = compilation.GetTypeByMetadataName(targetEnumClassName);
        Assert.Null(typeSymbol);
    }

    [Fact]
    public void WhenEnumMembersExist__StaticFieldWithSameNamesShouldExist()
    {
        var (compilation, _) = TestHelper.CompileConsole(@"using EnumClass.Attributes;

namespace Test;

[EnumClass]
public enum SampleEnum
{
    First,
    Second
}", @"using System;
using Test.EnumClass;

var first = SampleEnum.First;
Console.WriteLine(first);
");
        Assert.DoesNotContain(compilation.GetDiagnostics(), d => d.Severity == DiagnosticSeverity.Error);
    }
}