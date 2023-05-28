using System.Reflection;
using EnumClass.Attributes;
using EnumClass.Core;
using EnumClass.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace EnumClass.Generator.Tests;

public class EnumInfoFactoryTests
{
    public const string SampleEnumCode = @"using EnumClass.Attributes;

namespace Test;

[EnumClass]
public enum SampleEnum
{
    One = 1,
    Two = 2,
    Three = 3
}";

    private List<EnumInfo> GetEnumInfos(params string[] sourceCodes)
    {
        var compilation = CSharpCompilation.Create("Test", 
            syntaxTrees: sourceCodes.Select(x => CSharpSyntaxTree.ParseText(x)),
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumClassAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumInfo).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.GetCallingAssembly().Location),
                MetadataReference.CreateFromFile(typeof(string).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("netstandard")).Location),
            }, 
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        CSharpGeneratorDriver.Create(new EnumClassIncrementalGenerator())
                             .RunGeneratorsAndUpdateCompilation(compilation, out var resultCompilation, out var d);

        // Pass SourceProductionContext safely,
        // because there no errors expected
        return EnumInfoFactory.GetAllEnumInfosFromCompilation(resultCompilation, new SourceProductionContext())!;
    }

    [Fact]
    public void GetAllEnumsFromCompilation__WithSingleMarkedEnum__ShouldReturnListWithSingleElement()
    {
        var enums = GetEnumInfos(SampleEnumCode);
        Assert.Single(enums);
    }

    [Fact]
    public void GetAllEnumsFromCompilation__WithSingleMarkedEnumAnd3Members__ShouldReturnSingleEnumInfoWith3EnumMemberInfo()
    {
        var e = GetEnumInfos(SampleEnumCode).First();
        Assert.Equal(3, e.Members.Length);
    }

    [Fact]
    public void ShouldCorrectlyExtractClassName()
    {
        var e = GetEnumInfos(SampleEnumCode).First();
        Assert.Equal("SampleEnum", e.ClassName);
    }

    [Fact]
    public void ShouldCorrectlyExtractFullyQualifiedMemberClassNames()
    {
        var expected = new HashSet<string>()
        {
            "global::Test.EnumClass.SampleEnum.OneEnumValue",
            "global::Test.EnumClass.SampleEnum.TwoEnumValue",
            "global::Test.EnumClass.SampleEnum.ThreeEnumValue",
        };
        var e = GetEnumInfos(SampleEnumCode).First();
        var actual = e.Members.Select(x => x.FullyQualifiedClassName).ToHashSet();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldCorrectlyExtractMemberClassNames()
    {
        var expected = new HashSet<string>()
        {
            "OneEnumValue",
            "TwoEnumValue",
            "ThreeEnumValue",
        };
        var e = GetEnumInfos(SampleEnumCode).First();
        var actual = e.Members.Select(x => x.ClassName).ToHashSet();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldCorrectlyExtractFullyQualifiedEnumName()
    {
        var expected = "global::Test.SampleEnum";
        var e = GetEnumInfos(SampleEnumCode).First();
        var actual = e.FullyQualifiedEnumName;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldCorrectlyExtractFullyQualifiedEnumMemberValueName()
    {
        var expected = new HashSet<string>()
        {
            "global::Test.SampleEnum.One",
            "global::Test.SampleEnum.Two",
            "global::Test.SampleEnum.Three",
        };
        var e = GetEnumInfos(SampleEnumCode).First();
        var actual = e.Members.Select(x => x.FullyQualifiedEnumMemberName).ToHashSet();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldCorrectlyExtractNamespace()
    {
        var expected = "Test.EnumClass";
        var actual = GetEnumInfos(SampleEnumCode).First().Namespace;
        Assert.Equal(expected, actual);
    }

    public const string SecondEnumCode = @"using EnumClass.Attributes;

namespace Test;

[EnumClass]
public enum SecondEnum
{
    Nope,
    Yes,
    No
}";
    
    [Fact]
    public void GetAllEnumsFromCompilation_With2MarkedEnums_ShouldReturnListWith2Elements()
    {
        var enums = GetEnumInfos(SampleEnumCode, SecondEnumCode);
        Assert.Equal(2, enums.Count);
    }
}