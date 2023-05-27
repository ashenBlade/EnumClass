using System.Text.Json;
using EnumClass.JsonConverter.Generator.Tests.Integration.EnumClass;

namespace EnumClass.JsonConverter.Generator.Tests.Integration;

public class SampleEnumJsonConverterTests
{
    public static readonly JsonSerializerOptions SerializerOptions =
        new(JsonSerializerDefaults.General) {Converters = {new SampleEnumJsonConverter()}};

    private static EnumClass.SampleEnum? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<TestClass>(json, SerializerOptions)!.Value;
    }
    
    [Fact]
    public void Deserialize_WithValue0_ShouldDeserializeAsFirst()
    {
        var json = @"{
    ""Value"": 0
}";
        var actual = Deserialize(json);
        Assert.Equal(EnumClass.SampleEnum.First, actual);
    }
    
    
    [Fact]
    public void Deserialize_WithValue1_ShouldDeserializeAsSecond()
    {
        var json = @"{
    ""Value"": 1
}";
        var actual = Deserialize(json);
        Assert.Equal(EnumClass.SampleEnum.Second, actual);
    }
    
    
    [Fact]
    public void Deserialize_WithValue2_ShouldDeserializeAsThird()
    {
        var json = @"{
    ""Value"": 2
}";
        var actual = Deserialize(json);
        Assert.Equal(EnumClass.SampleEnum.Third, actual);
    }

    [Fact]
    public void Deserialize_WithoutValueInJson_ShouldDeserializeAsNull()
    {
        var json = @"{ }";
        var actual = Deserialize(json);
        Assert.Null(actual);
    }
    
}