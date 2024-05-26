using Autofac;
using Eshop.Basket.Api.Domain;
using Eshop.Basket.Api.Persistence;
using Eshop.Basket.Api.Services;
using Eshop.Common.Json;
using Eshop.Common.Web;
using Eshop.Discount.Grpc;
using Grpc.Net.ClientFactory;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace Eshop.Basket.Api;

public sealed class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        
        _webHostEnvironment = webHostEnvironment;

        _postgreConnectionString = GetConnectionString("Eshop");

        _redisConnectionString = GetConnectionString("EshopCache");
    }

    private readonly string _redisConnectionString;

    private readonly string _postgreConnectionString;

    private readonly IConfiguration _configuration;
    
    private readonly IWebHostEnvironment _webHostEnvironment;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWebCommon();

        services.AddMarten(ConfigureMarten)
            .UseLightweightSessions();

        services.AddStackExchangeRedisCache(ConfigureRedis);

        services.AddHealthChecks()
            .AddRedis(_redisConnectionString)
            .AddNpgSql(_postgreConnectionString);

        var discountClientBuilder = services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(ConfigureDiscountServiceClient);

        if (_webHostEnvironment.IsDevelopment())
        {
            discountClientBuilder.ConfigurePrimaryHttpMessageHandler(GetDevelopmentHttpClientHandler);
        }
    }

    private void ConfigureDiscountServiceClient(GrpcClientFactoryOptions options)
    {
        var uriString = _configuration.GetValue<string>("GrpcSettings:DiscountUri");

        ArgumentNullException.ThrowIfNull(uriString);

        options.Address = new(uriString);
    }

    private static HttpClientHandler GetDevelopmentHttpClientHandler()
    {
        return new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
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

        ArgumentNullException.ThrowIfNull(connectionString);

        return connectionString;
    }
}
