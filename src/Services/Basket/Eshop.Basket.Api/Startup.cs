using Autofac;
using Eshop.Basket.Api.Domain;
using Eshop.Basket.Api.Persistence;
using Eshop.Common.Json;
using Eshop.Common.Web;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Eshop.Basket.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;

        _redisConnectionString = GetConnectionString("Redis");
        
        _postgreConnectionString = GetConnectionString("Eshop");
    }

    private readonly string _redisConnectionString;

    private readonly string _postgreConnectionString;

    private readonly IConfiguration _configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWebCommon();

        services.AddMarten(ConfigureMarten)
            .UseLightweightSessions();

        services.AddStackExchangeRedisCache(ConfigureRedis);

        services.AddHealthChecks()
            .AddRedis(_redisConnectionString)
            .AddNpgSql(_postgreConnectionString);
    }

    private void ConfigureMarten(StoreOptions storeOptions)
    {
        storeOptions.Connection(_postgreConnectionString);

        storeOptions.UseSystemTextJsonForSerialization(casing: Casing.SnakeCase, configure: JsonSerializationConfigurator.Configure);
        
        ConfigureMartenSchema(storeOptions.Schema);
    }

    private static void ConfigureMartenSchema(MartenRegistry schema)
    {
        schema.For<ShoppingCart>()
            .Identity(shoppingCart => shoppingCart.UserId);
    }

    private void ConfigureRedis(RedisCacheOptions options)
    {
        var connectionString = GetConnectionString("Redis");

        options.Configuration = connectionString;
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.ConfigureWebCommon();

        builder.RegisterModule<PersistenceModule>();
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
