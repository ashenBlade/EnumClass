using System.Reflection;
using EnumClass.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SampleEnums;

namespace EnumClass.Generator.Tests;

public class ExternalEnumClassAttributeTests
{
    [Fact]
    public void WithSingleMember__ShouldGenerateWithoutErrors()
    {
        // References HelperProjects/SampleEnums/Token.cs
        var source = @"using EnumClass.Attributes;
using SampleEnums;

[assembly: ExternalEnumClass(typeof(Token), Namespace = ""SampleNamespace"")]
namespace Test;
";
        var compilation = CSharpCompilation.Create("Test", new[] {CSharpSyntaxTree.ParseText(source),},
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumClassAttribute).Assembly.Location),
                
                MetadataReference.CreateFromFile(Assembly.GetCallingAssembly().Location),
                MetadataReference.CreateFromFile(typeof(string).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("netstandard")).Location),
                MetadataReference.CreateFromFile(typeof(Token).Assembly.Location),
            }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                             .RunGeneratorsAndUpdateCompilation(compilation, out _, out var diagnostics);
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
    }
}