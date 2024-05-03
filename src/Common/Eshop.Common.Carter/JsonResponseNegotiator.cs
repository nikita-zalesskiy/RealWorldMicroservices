using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace Eshop.Common.Carter;

public sealed class JsonResponseNegotiator : IResponseNegotiator
{
    private readonly JsonOptions _options = new();

    public JsonResponseNegotiator()
    {
        JsonSerializationConfigurator.Configure(_options.SerializerOptions);
    }

    public bool CanHandle(MediaTypeHeaderValue accept)
    {
        return accept
            .MediaType
            .AsSpan()
            .Contains("json", StringComparison.OrdinalIgnoreCase);
    }

    public Task Handle<TModel>(HttpRequest request, HttpResponse response, TModel model, CancellationToken cancellationToken)
    {
        response.ContentType = "application/json; charset=utf-8";

        var serializedModel = JsonSerializer.Serialize(model, _options.SerializerOptions);

        return response.WriteAsync(serializedModel, cancellationToken);
    }
}
