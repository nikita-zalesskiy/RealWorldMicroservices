using AutoMapper;
using Eshop.Discount.Grpc.Domain;

namespace Eshop.Discount.Grpc.CompositionRoot;

internal sealed class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<decimal, string>()
            .ConvertUsing<ToStringInvariantCultureConverter<decimal>>();

        CreateMap<string, decimal>()
            .ConvertUsing<ParseStringInvariantCultureConverter<decimal>>();

        CreateMap<Coupon, CouponDto>();

        CreateMap<CouponDto, Coupon>();
    }
}
