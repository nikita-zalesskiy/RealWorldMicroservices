using Eshop.Discount.Grpc.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Discount.Grpc.CompositionRoot;

internal sealed class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        var coupons = GetCoupons();

        builder.HasData(coupons);

        builder.Field(coupon => coupon.CouponId);

        builder.Field(coupon => coupon.ProductId);

        builder.HasIndex(coupon => coupon.ProductId)
            .IsUnique();

        builder.Field(coupon => coupon.Amount);
    }

    private static List<Coupon> GetCoupons()
    {
        return
        [
            new()
            {
                CouponId = 1
                , ProductId = new Guid("9e9f7ac4-2768-4e6c-af21-226f51faa511")
                , Amount = 50.0m
            }
            , new()
            {
                CouponId = 2
                , ProductId = new Guid("27cbb917-1c47-443e-bbea-a868f0d99da1")
                , Amount = 55.0m
            }
        ];
    }
}
