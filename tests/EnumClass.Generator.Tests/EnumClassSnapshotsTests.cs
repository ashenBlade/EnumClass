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
        return CompileSourceCode(source);
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
        return CompileSourceCode(source);
    }
    
    [Fact]
    public Task With5EnumValuesAndDisplayAttribute__ShouldGenerateCodeWithChangedToStringRepresentation()
    {
        var source = @"using EnumClass.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Sample 
{
    [EnumClass]
    public enum PetKind
    {
        [StringValue(""Sample cat"")]
        Cat,
        Dog,
        Parrot,
        [StringValue(""Big Boy"")]
        Hamster,
        Crocodile
    }
}";
        return CompileSourceCode(source);
    }
    
    private static Task CompileSourceCode(string sourceCode)
    {
        var syntaxTree          = CSharpSyntaxTree.ParseText(sourceCode);
        var compilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: new[] { syntaxTree },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            });

        var cSharpGeneratorDriver = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());

        var generalDriver = cSharpGeneratorDriver.RunGenerators(compilation);
        return Verify(generalDriver)
              .UseDirectory("Snapshots")
              .UseUniqueDirectory();
    }
}