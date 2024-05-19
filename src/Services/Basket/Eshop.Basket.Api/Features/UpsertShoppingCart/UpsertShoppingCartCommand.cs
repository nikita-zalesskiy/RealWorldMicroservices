using Eshop.Basket.Api.Domain;
using Eshop.Common.Cqrs;

namespace Eshop.Basket.Api.Features.UpsertShoppingCart;

public sealed class UpsertShoppingCartCommand : ICommand<UpsertShoppingCartCommandResult>
{
    public UpsertShoppingCartCommand(ShoppingCart shoppingCart)
    {
        ShoppingCart = shoppingCart;
    }
    
    public readonly ShoppingCart ShoppingCart;
}
