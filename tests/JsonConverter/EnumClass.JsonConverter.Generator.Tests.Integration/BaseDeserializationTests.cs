using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnumClass.JsonConverter.Generator.Tests.Integration;

public abstract class BaseDeserializationTests<TEnumClass>
{
    protected abstract JsonConverter<TEnumClass> Converter { get; }
    protected JsonSerializerOptions SerializerOptions => new(JsonSerializerDefaults.General) {Converters = {Converter}};
    
    protected void CheckDeserialization(string json, TEnumClass expected)
    {
        var actual = JsonSerializer.Deserialize<GenericDeserializationClass<TEnumClass>>(json, SerializerOptions)!;
        Assert.Equal(expected, actual.Value);
    }
    
    protected abstract IEnumerable<(string Json, TEnumClass Expected)> EnumValues { get; }

    [Fact]
    public void Deserialize__WithValidValues__ShouldDeserializeToCorrectValues()
    {
        foreach (var (json, expected) in EnumValues)
        {
            CheckDeserialization(json, expected);
        }
    }

    [Fact]
    public void Deserialize__WithoutProvidedValue__ShouldDeserializeAsNull()
    {
        var json = "{}";
        var actual = JsonSerializer.Deserialize<GenericDeserializationClass<TEnumClass>>(json, SerializerOptions)!.Value;
        Assert.Null(actual);
    }
}