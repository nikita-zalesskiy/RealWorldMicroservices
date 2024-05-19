using System.Diagnostics.CodeAnalysis;

namespace Eshop.Basket.Api.Domain;

public sealed class ShoppingCart
{
    private ShoppingCart()
    {
        
    }

    [SetsRequiredMembers]
    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }

    public required Guid UserId;

    public List<ShoppingCartItem> Items = [];

    public decimal TotalPrice => GetTotalPrice();

    private decimal GetTotalPrice()
    {
        return Items.Sum(cartItem => cartItem.Price * cartItem.Quantity);
    }
}
