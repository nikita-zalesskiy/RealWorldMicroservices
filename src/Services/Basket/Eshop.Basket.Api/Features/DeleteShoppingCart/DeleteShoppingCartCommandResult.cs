namespace Eshop.Basket.Api.Features.DeleteShoppingCart;

public sealed class DeleteShoppingCartCommandResult
{
    public DeleteShoppingCartCommandResult(bool isSucceeded)
    {
        IsSucceeded = isSucceeded;
    }

    public readonly bool IsSucceeded;
}
