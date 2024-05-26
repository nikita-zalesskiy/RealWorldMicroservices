using Autofac;
using Catalog.Api.Domain;
using Eshop.Catalog.Api.Persistence;
using Eshop.Common.Json;
using Eshop.Common.Web;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Eshop.Catalog.Api;

public sealed class Startup
{
    public Startup(
        IConfiguration configuration
        , IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        
        _webHostEnvironment = webHostEnvironment;

        _connectionString = GetConnectionString("Eshop");
    }

    private readonly IConfiguration _configuration;

    private readonly string _connectionString;
    
    private readonly IWebHostEnvironment _webHostEnvironment;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWebCommon();

        services.AddMarten(ConfigureMarten)
            .UseLightweightSessions();

        if (_webHostEnvironment.IsDevelopment())
        {
            services.InitializeMartenWith<ProductDataSeed>();
        }

        services.AddHealthChecks()
            .AddNpgSql(_connectionString);
    }

    private void ConfigureMarten(StoreOptions storeOptions)
    {
        storeOptions.Connection(_connectionString);

        storeOptions.UseSystemTextJsonForSerialization(casing: Casing.SnakeCase, configure: JsonSerializationConfigurator.Configure);

        ConfigureMartenSchema(storeOptions.Schema);
    }

    private static void ConfigureMartenSchema(MartenRegistry schema)
    {
        schema.For<Product>()
            .Identity(product => product.ProductId);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.ConfigureWebCommon();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseWebCommon();

        var healthCheckOptions = new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        };

        applicationBuilder.UseHealthChecks("/health", healthCheckOptions);
    }

    private string GetConnectionString(string connectionStringKey)
    {
        var connectionString = _configuration.GetConnectionString(connectionStringKey);

        if (connectionString is null)
        {
            throw new NotSupportedException(nameof(connectionString));
        }

        return connectionString;
    }
}
