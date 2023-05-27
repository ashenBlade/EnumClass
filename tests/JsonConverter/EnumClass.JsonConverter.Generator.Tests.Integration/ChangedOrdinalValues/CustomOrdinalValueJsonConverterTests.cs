using System.Text.Json.Serialization;
using EnumClass.JsonConverter.Generator.Tests.Integration.ChangedOrdinalValues.EnumClass;

namespace EnumClass.JsonConverter.Generator.Tests.Integration.ChangedOrdinalValues;

public class CustomOrdinalValueJsonConverterTests: BaseJsonConverterTests<EnumClass.CustomOrdinalValue>
{
    protected override JsonConverter<EnumClass.CustomOrdinalValue> Converter => new CustomOrdinalValueJsonConverter();
    protected override IEnumerable<(string Json, EnumClass.CustomOrdinalValue Expected)> GivenJsonWithExpectedValue => new (string, EnumClass.CustomOrdinalValue)[]
    {
        (@"{""Value"": 1}", EnumClass.CustomOrdinalValue.One),
        (@"{""Value"": 10}", EnumClass.CustomOrdinalValue.Ten),
        (@"{""Value"": 50}", EnumClass.CustomOrdinalValue.Fifty),
    };

    protected override IEnumerable<(EnumClass.CustomOrdinalValue Value, string Expected)> GivenValueWithExpectedJson =>
        new (EnumClass.CustomOrdinalValue, string)[]
        {
            (EnumClass.CustomOrdinalValue.One, @"{""Value"":1}"),
            (EnumClass.CustomOrdinalValue.Ten, @"{""Value"":10}"),
            (EnumClass.CustomOrdinalValue.Fifty, @"{""Value"":50}"),
        };
}