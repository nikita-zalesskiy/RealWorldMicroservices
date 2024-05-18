using Autofac;
using Eshop.Catalog.Api.DataSeeds;
using Eshop.Common.Json;
using Eshop.Common.Web;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Eshop.Catalog.Api;

public class Startup
{
    public Startup(
        IConfiguration configuration
        , IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        
        _webHostEnvironment = webHostEnvironment;

        _connectionString = new(GetConnectionString);
    }

    private readonly IConfiguration _configuration;

    private readonly Lazy<string> _connectionString;
    
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
            .AddNpgSql(_connectionString.Value);
    }

    private void ConfigureMarten(StoreOptions storeOptions)
    {
        storeOptions.Connection(_connectionString.Value);

        storeOptions.UseSystemTextJsonForSerialization(casing: Casing.SnakeCase, configure: JsonSerializationConfigurator.Configure);
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

    private string GetConnectionString()
    {
        var connectionString = _configuration.GetConnectionString("Eshop");

        if (connectionString is null)
        {
            throw new NotSupportedException(nameof(connectionString));
        }

        return connectionString;
    }
}
