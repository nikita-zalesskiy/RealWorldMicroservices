using Eshop.Basket.Api.Domain;
using Eshop.Basket.Api.Persistence;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Eshop.Discount.Grpc;
using System.Globalization;

namespace Eshop.Basket.Api.Features.UpsertShoppingCart;

internal sealed class UpsertShoppingCartCommandHandler : ICommandHandler<UpsertShoppingCartCommand, UpsertShoppingCartCommandResult>
{
    public UpsertShoppingCartCommandHandler(
        RedisShoppingCartRepository shoppingCartRepository
        , DiscountProtoService.DiscountProtoServiceClient discountServiceClient)
    {
        _discountServiceClient = discountServiceClient;

        _shoppingCartRepository = shoppingCartRepository;
    }
    
    private readonly RedisShoppingCartRepository _shoppingCartRepository;

    private readonly DiscountProtoService.DiscountProtoServiceClient _discountServiceClient;

    public async Task<RequestResult<UpsertShoppingCartCommandResult>> Handle(UpsertShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = command.ShoppingCart;

        await ApplyDiscounts(shoppingCart.Items, cancellationToken);

        await _shoppingCartRepository.UpsertShoppingCartAsync(shoppingCart, cancellationToken);

        var commandResult = new UpsertShoppingCartCommandResult(shoppingCart.UserId);

        return commandResult.AsRequestResult();
    }

    private async Task ApplyDiscounts(List<ShoppingCartItem> cartItems, CancellationToken cancellationToken)
    {
        foreach (var cartItem in cartItems)
        {
            var getDiscountRequest = new GetDiscountRequest
            {
                ProductId = cartItem.ProductId.ToString()
            };

            var getDiscountResponse = await _discountServiceClient
                .GetDiscountAsync(getDiscountRequest, cancellationToken: cancellationToken);

            var couponDto = getDiscountResponse.Coupon;

            if (!decimal.TryParse(couponDto.Amount, CultureInfo.InvariantCulture, out var couponAmount))
            {
                continue;
            }

            cartItem.Price -= couponAmount;

            cartItem.Price = Math.Max(cartItem.Price, decimal.Zero);
        }
    }
}
