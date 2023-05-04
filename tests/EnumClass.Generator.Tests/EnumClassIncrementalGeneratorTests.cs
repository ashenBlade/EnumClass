using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using EnumClass.Generator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

public class EnumClassIncrementalGeneratorTests
{
    private static readonly CSharpCompilationOptions DllCompilationOptions = new(OutputKind.DynamicallyLinkedLibrary);
    private static readonly CSharpCompilationOptions ConsoleAppCompilationOptions = new(OutputKind.ConsoleApplication);

    private static (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) CompileSourceCode(string[] sources, MetadataReference[] references, CSharpCompilationOptions compilationOptions)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: sources.Select(s => CSharpSyntaxTree.ParseText(s)),
            references: references,
            options: compilationOptions);
        var generator = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());
        generator.RunGeneratorsAndUpdateCompilation(compilation, out var postGeneratorCompilation, out var generatorDiagnostics);
        return ( postGeneratorCompilation, generatorDiagnostics );
    } 
    
    
    [Fact]
    public void WhenNoEnumClassAttributes__ShouldCompileWithoutErrors()
    {
        var entryPointCode = 
            @"using System;

Console.WriteLine(""Hello, world"");";
        var syntaxTree = CSharpSyntaxTree.ParseText(entryPointCode);
        var originCompilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: new[] { syntaxTree },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            },
            ConsoleAppCompilationOptions);
        var generator = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());
        generator.RunGeneratorsAndUpdateCompilation(originCompilation, out _, out var diagnostics);
        Assert.DoesNotContain(diagnostics, d => d.Severity == DiagnosticSeverity.Error);
    }

    [Fact]
    public void WhenEnumWithoutAttribute__ShouldNotCreateEnumClass()
    {
        var enumWithoutAttribute = @"
namespace Sample
{
    public enum ThisIsEnum
    {
        Value = 1,
        Val = 2
    }
}
";
    }
    
    [Fact]
    public void WhenSingleEnumClassAttributeWithCorrectEnum__ShouldCompileWithoutErrors()
    {
        var enumDeclarationCode =
            @"using System;
using EnumClass.Attributes;

namespace Sample
{
    [EnumClass]
    public enum PetKind
    {
        Cat,
        Dog,
        Hamster,
        Parrot
    }
}
";
        var originCompilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(enumDeclarationCode)
            },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            },
            DllCompilationOptions);
        var generator = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());
        generator.RunGeneratorsAndUpdateCompilation(originCompilation, out var processedCompilation, out _);
        var diagnostics = processedCompilation.GetDiagnostics();
        Assert.DoesNotContain(diagnostics, d => d.Severity == DiagnosticSeverity.Error);
    }
    
    [Fact]
    public void WhenSingleEnumClassAttributeWithCorrectEnum__ShouldCreateEnumClasses()
    {
        var enumDeclarationCode =
            @"using System;
using EnumClass.Attributes;

namespace Sample
{
    [EnumClass]
    public enum PetKind
    {
        Cat,
        Dog,
        Hamster,
        Parrot
    }
}
";
        var entryPointCode = @"using System;
using Sample.EnumClass;

var cat = PetKind.Cat;
System.Console.WriteLine(cat);
";

        var originCompilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(enumDeclarationCode),
                CSharpSyntaxTree.ParseText(entryPointCode)
            },
            references: new[]
            {
                GetSystemPrivateCorLibReference(),
                GetSystemConsoleReference(),
                GetSystemRuntimeReference()
            },
            ConsoleAppCompilationOptions);
        var generator = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());
        generator.RunGeneratorsAndUpdateCompilation(originCompilation, out var postGeneratorCompilation, out _);
        var diagnostics = postGeneratorCompilation.GetDiagnostics();
        Assert.DoesNotContain(diagnostics, d => d.Severity == DiagnosticSeverity.Error);
    }

    [Theory]
    [InlineData("Cat")]
    [InlineData("Cat", "Dog")]
    [InlineData("Sample", "Value", "To", "Test", "Theory")]
    public void WhenSingleEnumClass__ShouldGenerateClassesWithIdenticalNamesAsInEnum(params string[] names)
    {
        var enumDeclaration = new StringBuilder(@"using EnumClass.Attributes;
namespace Sample
{
    [EnumClass]
    public enum SampleEnum
    {
");
        var entryPointCode = new StringBuilder(@"using Sample.EnumClass;
using System;

");
        foreach (var name in names)
        {
            enumDeclaration.Append($"        {name},\n");
            entryPointCode.Append($"Console.WriteLine(SampleEnum.{name}.ToString());\n");
        }

        enumDeclaration.Append("    }\n}");
        var text = entryPointCode.ToString();
        var s = enumDeclaration.ToString();
        var originCompilation = CSharpCompilation.Create(
            assemblyName: "TestAssembly",
            syntaxTrees: new[]
            {
                CSharpSyntaxTree.ParseText(s),
                CSharpSyntaxTree.ParseText(text)
            },
            references: new[]
            {
                GetSystemPrivateCorLibReference(),
                GetSystemConsoleReference(),
                GetSystemRuntimeReference()
            },
            ConsoleAppCompilationOptions);
        
        var generator = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator());
        generator.RunGeneratorsAndUpdateCompilation(originCompilation, out var postGeneratorCompilation, out _);
        var diagnostics = postGeneratorCompilation.GetDiagnostics();
        Assert.DoesNotContain(diagnostics, d => d.Severity == DiagnosticSeverity.Error);
    }

    private static PortableExecutableReference GetSystemPrivateCorLibReference()
    {
        return MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
    }

    private static PortableExecutableReference GetSystemConsoleReference()
    {
        return MetadataReference.CreateFromFile(typeof(Console).Assembly.Location);
    }

    private static PortableExecutableReference GetSystemRuntimeReference()
    {
        return MetadataReference.CreateFromFile(Path.Combine( Path.GetDirectoryName(typeof(object).Assembly.Location)!, "System.Runtime.dll" ));
    }
}