using System.Text.Json;
using System.Text.Json.Serialization;
using EnumClass.JsonConverter.Generator.Tests.Integration.ChangedOrdinalValues.EnumClass;

namespace EnumClass.JsonConverter.Generator.Tests.Integration;

public abstract class BaseJsonConverterTests<TEnumClass> where TEnumClass : class
{
    protected abstract JsonConverter<TEnumClass> Converter { get; }
    protected JsonSerializerOptions SerializerOptions => new(JsonSerializerDefaults.General) {Converters = {Converter}};
    
    protected void CheckDeserialization(string json, TEnumClass expected)
    {
        var actual = JsonSerializer.Deserialize<GenericJsonConverterClass<TEnumClass>>(json, SerializerOptions)!;
        Assert.Equal(expected, actual.Value);
    }
    
    protected abstract IEnumerable<(string Json, TEnumClass Expected)> GivenJsonWithExpectedValue { get; }

    [Fact]
    public void Deserialize__WithValidValues__ShouldDeserializeToCorrectValues()
    {
        foreach (var (json, expected) in GivenJsonWithExpectedValue)
        {
            CheckDeserialization(json, expected);
        }
    }

    [Fact]
    public void Deserialize__WithoutProvidedValue__ShouldDeserializeAsNull()
    {
        var json = "{}";
        var actual = JsonSerializer.Deserialize<GenericJsonConverterClass<TEnumClass>>(json, SerializerOptions)!.Value;
        Assert.Null(actual);
    }
    
    protected abstract IEnumerable<(TEnumClass Value, string Expected)> GivenValueWithExpectedJson { get; }
    
    [Fact]
    public void Serialize__WithValidValue__ShouldSerializeAsExpected()
    {
        foreach (var (value, expected) in GivenValueWithExpectedJson)
        {
            // By default, serialized json is compact
            // Also General setting serialize with strict equal property names
            var actual = JsonSerializer.Serialize(new GenericJsonConverterClass<TEnumClass>()
            {
                Value = value
            }, SerializerOptions);
            Assert.Equal(expected, actual);
        }
    }

    [Fact]
    public void Serialize_WithNullValue_ShouldSerializeAsNull()
    {
        var expected = @"{""Value"":null}";
        var actual = JsonSerializer.Serialize(new GenericJsonConverterClass<TEnumClass>() {Value = null},
            SerializerOptions);
        Assert.Equal(expected, actual);
    }
}