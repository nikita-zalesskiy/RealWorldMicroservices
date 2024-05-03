using Eshop.Common.Carter;
using Marten;
using Microsoft.AspNetCore.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Eshop.Catalog.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<JsonOptions>(ConfigureJsonOptions);
        
        services.AddCarter(configurator: ConfigureCarter);

        services
            .AddMarten(ConfigureMarten)
            .UseLightweightSessions();

        services.AddMediatR(ConfigureMediatR);
    }

    private void ConfigureJsonOptions(JsonOptions options)
    {
        JsonSerializationConfigurator.Configure(options.SerializerOptions);
    }

    private void ConfigureCarter(CarterConfigurator configurator)
    {
        configurator.WithResponseNegotiator<JsonResponseNegotiator>();
    }

    private void ConfigureMarten(StoreOptions storeOptions)
    {
        var connectionString = _configuration.GetConnectionString("Eshop");

        if (connectionString is null)
        {
            throw new NotSupportedException(nameof(connectionString));
        }

        storeOptions.Connection(connectionString);

        storeOptions.UseSystemTextJsonForSerialization(casing: Casing.SnakeCase, configure: JsonSerializationConfigurator.Configure);
    }

    private static void ConfigureMediatR(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();

        applicationBuilder.UseEndpoints(ConfigureEndpoints);
    }

    private static void ConfigureEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapCarter();
    }
}
