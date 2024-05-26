using AutoMapper;
using Eshop.Discount.Grpc.Domain;
using Eshop.Discount.Grpc.Persistence;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Discount.Grpc.Services;

public sealed class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    public DiscountService(IMapper mapper
        , DiscountContext discountContext)
    {
        _mapper = mapper;
        
        _discountContext = discountContext;
    }

    private readonly IMapper _mapper;
    
    private readonly DiscountContext _discountContext;

    public override async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var cancellationToken = context.CancellationToken;

        if (!Guid.TryParse(request.ProductId, out var productId))
        {
            throw new ArgumentException(message: string.Empty, paramName: nameof(request));
        }

        var coupon = await _discountContext.Coupons
            .FirstOrDefaultAsync(coupon => productId.Equals(coupon.ProductId), cancellationToken);

        coupon ??= new()
        {
            ProductId = productId
        };

        var couponDto = _mapper.Map<Coupon, CouponDto>(coupon);

        return new()
        {
            Coupon = couponDto
        };
    }

    public override async Task<CreateDiscountResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var cancellationToken = context.CancellationToken;

        var coupon = _mapper.Map<CouponDto, Coupon>(request.Coupon);

        await _discountContext.AddAsync(coupon, cancellationToken);

        await _discountContext.SaveChangesAsync(cancellationToken);

        return new()
        {
            CouponId = coupon.CouponId
        };
    }

    public override async Task<UpdateDiscountResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<CouponDto, Coupon>(request.Coupon);

        _discountContext.Update(coupon);

        await _discountContext.SaveChangesAsync(context.CancellationToken);

        return new()
        {
            IsSucceeded = true
        };
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var cancellationToken = context.CancellationToken;

        if (!Guid.TryParse(request.ProductId, out var productId))
        {
            throw new ArgumentException(message: string.Empty, paramName: nameof(request));
        }

        await _discountContext.Coupons
            .Where(coupon => productId.Equals(coupon.ProductId))
            .ExecuteDeleteAsync();

        await _discountContext.SaveChangesAsync(cancellationToken);

        return new()
        {
            IsSucceeded = true
        };
    }
}
