using System.Text.Json.Serialization;
using System.Text.Json;

namespace Eshop.Common.Json;

public static class JsonSerializationConfigurator
{
    public static void Configure(JsonSerializerOptions serializationOptions)
    {
        var casePolicy = JsonNamingPolicy.SnakeCaseLower;

        serializationOptions.IncludeFields = true;

        serializationOptions.DictionaryKeyPolicy = casePolicy;
        serializationOptions.PropertyNamingPolicy = casePolicy;
        serializationOptions.PropertyNameCaseInsensitive = true;

        serializationOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;

        serializationOptions.Converters.Add(new JsonStringEnumConverter(casePolicy));

        serializationOptions.AllowTrailingCommas = true;

        serializationOptions.WriteIndented = true;
    }
}
