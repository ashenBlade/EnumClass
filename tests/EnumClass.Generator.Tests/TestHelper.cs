using System.Collections.Immutable;
using EnumClass.Attributes;
using EnumClass.Generator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

public static class TestHelper
{
    private static CSharpCompilationOptions ConsoleOptions = new(OutputKind.ConsoleApplication);
    private static CSharpCompilationOptions DllOptions = new(OutputKind.DynamicallyLinkedLibrary);
    
    public static (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) CompileDll(params string[] sources)
    {
        return CompileCore(sources, DllOptions, 
            new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumClassAttribute).Assembly.Location), 
            });
    }
 
    public static (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) CompileConsole(params string[] sources)
    {
        return CompileCore(sources, ConsoleOptions, new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(EnumClassAttribute).Assembly.Location), 
            MetadataReference.CreateFromFile(typeof(ConsoleColor).Assembly.Location), 
        });
    }
    
    private static (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) CompileCore(string[] sources, CSharpCompilationOptions compilationOptions, MetadataReference[] metadataReferences)
    {
        var compilation = CSharpCompilation.Create(
            assemblyName: "Test",
            syntaxTrees: sources.Select(s => CSharpSyntaxTree.ParseText(s)), 
            references: metadataReferences,
            options: compilationOptions);
        CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                             .RunGeneratorsAndUpdateCompilation(compilation, out var output, out var diagnostics);
        return ( output, diagnostics );
    }

}