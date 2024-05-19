using Eshop.Basket.Api.Domain;

namespace Eshop.Basket.Api.Features.GetShoppingCart;

public sealed class GetShoppingCartQueryResult
{
    public GetShoppingCartQueryResult(ShoppingCart? shoppingCart)
    {
        ShoppingCart = shoppingCart;
    }

    public readonly ShoppingCart? ShoppingCart;

}
