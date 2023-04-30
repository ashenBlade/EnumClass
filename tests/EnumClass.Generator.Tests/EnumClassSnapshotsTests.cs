using EnumClass.Generator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

/// <summary>
/// These tests are needed primarily
/// to <b>visualize</b> the generated code -
/// not for testing business logic
/// </summary>
[UsesVerify]
public class EnumClassSnapshotsTests
{
    [Fact]
    public Task With1EnumValue__ShouldGenerateCorrectly()
    {
        var source = @"using EnumClass.Attributes;

namespace Sample 
{
    [EnumClass]
    public enum PetKind
    {
        Cat
    }
}";
        return SnapshotTestHelper.CompileSourceCode(source);
    }

    [Fact]
    public Task With2EnumValuesAndAssignedOrdinalValue__ShouldGenerateCodeWithChangedValue()
    {
        var source = @"using EnumClass.Attributes;

namespace Sample 
{
    [EnumClass]
    public enum PetKind
    {
        Cat,
        Dog = 42
    }
}";
        return SnapshotTestHelper.CompileSourceCode(source);
    }
    
    [Fact]
    public Task With5EnumValuesAndDisplayAttribute__ShouldGenerateCodeWithChangedToStringRepresentation()
    {
        var source = @"using EnumClass.Attributes;
using System.ComponentModel.Attributes;

namespace Sample 
{
    [EnumClass]
    public enum PetKind
    {
        [Display(Name = ""Sample cat"")]
        Cat,
        Dog,
        Parrot,
        Hamster
    }
}";
        return SnapshotTestHelper.CompileSourceCode(source);
    }
}