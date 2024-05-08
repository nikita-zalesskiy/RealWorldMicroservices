using Eshop.Common.Reflection;
using Optional;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eshop.Common.Web.Functional;

public sealed class OptionJsonConverterFactory : JsonConverterFactory
{
    private static readonly Type s_optionType = typeof(Option<>);

    private static readonly Type s_optionJsonConverterType = typeof(OptionJsonConverter<>);

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsClosedVersionOf(s_optionType);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert.GetGenericArguments() is not [var valueType])
        {
            throw new ArgumentException(message: nameof(CreateConverter));
        }

        var jsonConverterType = s_optionJsonConverterType.MakeGenericType(valueType);

        return (JsonConverter?) Activator.CreateInstance(jsonConverterType);
    }
}
