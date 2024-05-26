using Eshop.Discount.Grpc.Persistence;
using Eshop.Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Eshop.Discount.Grpc;

public sealed class Startup
{
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;

        _connectionString = GetConnectionString("Eshop");
    }

    private readonly string _connectionString;

    private readonly IConfiguration _configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddGrpc();

        services.AddDbContext<DiscountContext>(ConfigureDiscountContext);
    }

    private void ConfigureDiscountContext(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite(_connectionString);
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.Migrate<DiscountContext>();

        applicationBuilder.UseRouting();

        applicationBuilder.UseEndpoints(ConfigureEndpoints);
    }

    private static void ConfigureEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGrpcService<DiscountService>();
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