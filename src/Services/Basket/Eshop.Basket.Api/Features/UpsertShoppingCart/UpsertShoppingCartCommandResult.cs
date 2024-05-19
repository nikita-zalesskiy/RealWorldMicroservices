namespace Eshop.Basket.Api.Features.UpsertShoppingCart;

public sealed class UpsertShoppingCartCommandResult
{
    public UpsertShoppingCartCommandResult(Guid userId)
    {
        UserId = userId;
    }
    
    public readonly Guid UserId;
}
