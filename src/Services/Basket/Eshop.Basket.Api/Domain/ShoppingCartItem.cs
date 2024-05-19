namespace Eshop.Basket.Api.Domain;

public sealed class ShoppingCartItem
{
    public required Guid ProductId;
    
    //public required string ProductName;
    
    public required int Quantity;

    //public required string Color;

    public required decimal Price;
}
