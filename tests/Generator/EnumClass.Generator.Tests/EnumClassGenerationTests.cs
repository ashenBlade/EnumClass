using System.Reflection;
using System.Runtime.InteropServices;
using EnumClass.Attributes;
using EnumClass.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

public class EnumClassGenerationTests
{
    [Fact]
    public void WithSingleMember__ShouldGenerateCorrectly()
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
                // MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Linq.Expressions")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("netstandard")).Location),
            }, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        var driver = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                                          .RunGenerators(compilation);
    }
    
    
    [Fact]
    public void WithTwoMembers__ShouldGenerateCorrectly()
    {
        var source = @"using EnumClass.Attributes;

namespace Test;

[EnumClass(Namespace = ""Test"", ClassName = ""SampleEnum"")]
public enum SampleEnum: long
{
    Manager = long.MaxValue - 4,
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
        
        var driver = CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                                          .RunGenerators(compilation);
    }
}