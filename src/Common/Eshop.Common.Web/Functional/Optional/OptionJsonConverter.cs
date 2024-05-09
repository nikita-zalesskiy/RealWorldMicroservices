using Eshop.Common.Functional;
using Optional;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eshop.Common.Web.Functional;

public sealed class OptionJsonConverter<TValue> : JsonConverter<Option<TValue>>
{
    public override Option<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = JsonSerializer.Deserialize<TValue>(ref reader, options);

        return value!.SomeNotNull();
    }

    public override void Write(Utf8JsonWriter writer, Option<TValue> option, JsonSerializerOptions options)
    {
        if (!option.TryGetValue(out var value))
        {
            return;
        }

        JsonSerializer.Serialize(writer, value, options);
    }
}
