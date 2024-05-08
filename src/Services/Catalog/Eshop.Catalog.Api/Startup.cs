using Autofac;
using Eshop.Common.Carter;
using Eshop.Common.Json;
using Eshop.Common.Web.Functional;
using Eshop.Common.Web.Validation;
using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.Json;
using System.Reflection;

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

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddCarter(configurator: ConfigureCarter);

        services
            .AddMarten(ConfigureMarten)
            .UseLightweightSessions();

        services.AddMediatR(ConfigureMediatR);
    }

    private void ConfigureJsonOptions(JsonOptions options)
    {
        var serializationOptions = options.SerializerOptions;

        var jsonConverters = serializationOptions.Converters;

        JsonSerializationConfigurator.Configure(serializationOptions);

        jsonConverters.Add(new OptionJsonConverterFactory());
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

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule<ValidationModule>();
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
