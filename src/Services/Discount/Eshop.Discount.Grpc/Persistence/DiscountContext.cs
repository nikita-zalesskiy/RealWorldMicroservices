using Eshop.Discount.Grpc.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Eshop.Discount.Grpc.Persistence;

public class DiscountContext : DbContext
{
    public DiscountContext(DbContextOptions options) : base(options)
    {

    }

    public required DbSet<Coupon> Coupons { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = Assembly.GetExecutingAssembly();

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
