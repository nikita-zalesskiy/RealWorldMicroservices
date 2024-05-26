using Microsoft.EntityFrameworkCore;

namespace Eshop.Discount.Grpc;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder Migrate<TDbContext>(this IApplicationBuilder applicationBuilder)
        where TDbContext : DbContext
    {
        using var serviceScope = applicationBuilder
            .ApplicationServices
            .CreateScope();

        using var dbContext = serviceScope
            .ServiceProvider
            .GetRequiredService<TDbContext>();

        dbContext
            .Database
            .Migrate();

        return applicationBuilder;
    }
}
