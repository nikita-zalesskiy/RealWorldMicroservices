namespace Eshop.Discount.Grpc.Domain;

public sealed class Coupon
{
    public int CouponId;

    public required Guid ProductId;

    public decimal Amount;
}
