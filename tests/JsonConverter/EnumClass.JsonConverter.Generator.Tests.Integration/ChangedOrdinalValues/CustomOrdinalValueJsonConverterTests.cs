using System.Text.Json.Serialization;
using EnumClass.JsonConverter.Generator.Tests.Integration.ChangedOrdinalValues.EnumClass;

namespace EnumClass.JsonConverter.Generator.Tests.Integration.ChangedOrdinalValues;

public class CustomOrdinalValueJsonConverterTests: BaseDeserializationTests<EnumClass.CustomOrdinalValue>
{
    protected override JsonConverter<EnumClass.CustomOrdinalValue> Converter => new CustomOrdinalValueJsonConverter();
    protected override IEnumerable<(string Json, EnumClass.CustomOrdinalValue Expected)> EnumValues => new (string, EnumClass.CustomOrdinalValue)[]
    {
        (@"{""Value"": 1}", EnumClass.CustomOrdinalValue.One),
        (@"{""Value"": 10}", EnumClass.CustomOrdinalValue.Ten),
        (@"{""Value"": 50}", EnumClass.CustomOrdinalValue.Fifty),
    };
}