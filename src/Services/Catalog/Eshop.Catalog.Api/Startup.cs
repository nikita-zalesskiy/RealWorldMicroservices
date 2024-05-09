using Autofac;
using Eshop.Common.Json;
using Eshop.Common.Web;
using FluentValidation;
using Marten;
using System.Reflection;

namespace Eshop.Catalog.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private readonly IConfiguration _configuration;
    
    private static readonly Assembly s_executingAssembly = Assembly.GetExecutingAssembly();

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWebCommon();

        services.AddValidatorsFromAssembly(s_executingAssembly);

        services.AddMarten(ConfigureMarten)
            .UseLightweightSessions();

        services.AddMediatR(ConfigureMediatR);

        services.AddAutoMapper(s_executingAssembly);
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
        configuration.RegisterServicesFromAssembly(s_executingAssembly);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.ConfigureWebCommon();
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
