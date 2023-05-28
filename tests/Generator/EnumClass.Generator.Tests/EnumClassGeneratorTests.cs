using System.Reflection;
using EnumClass.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

public class EnumClassGeneratorTests
{
    [Fact]
    public void WithSingleMember__ShouldGenerateWithoutErrors()
    {
        var source = @"using EnumClass.Attributes;

namespace Test;

[EnumClass]
public enum SampleEnum: byte
{
    First
}";
        var compilation = CSharpCompilation.Create("Test", new[] {CSharpSyntaxTree.ParseText(source),},
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumClassAttribute).Assembly.Location),
                
                MetadataReference.CreateFromFile(Assembly.GetCallingAssembly().Location),
                MetadataReference.CreateFromFile(typeof(string).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("netstandard")).Location),
            }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                             .RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
    }
    
    
    [Fact]
    public void WithSpecifiedAttributeArguments__ShouldGenerateWithoutErrors()
    {
        var source = @"using EnumClass.Attributes;

namespace Test;

[EnumClass(Namespace = ""Test"", ClassName = ""SampleEnum"")]
public enum SampleEnum
{
    Manager,
    Programmer,
    Tester,
    CTO,
    CEO,
}";
        var compilation = CSharpCompilation.Create("Test", new[] {CSharpSyntaxTree.ParseText(source),},
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumClassAttribute).Assembly.Location),
                
                MetadataReference.CreateFromFile(Assembly.GetCallingAssembly().Location),
                MetadataReference.CreateFromFile(typeof(string).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("netstandard")).Location),
            }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                             .RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
    }
}