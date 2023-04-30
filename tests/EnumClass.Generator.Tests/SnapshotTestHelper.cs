using EnumClass.Generator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

public static class SnapshotTestHelper
{
    public static Task CompileSourceCode(string sourceCode)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var compilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: new[] {syntaxTree},
            references: new[]{ MetadataReference.CreateFromFile(typeof(object).Assembly.Location)});

        var cSharpGeneratorDriver = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());

        var generalDriver = cSharpGeneratorDriver.RunGenerators(compilation);
        return Verify(generalDriver)
             .UseDirectory("Snapshots")
             .UseUniqueDirectory();
    }
}