namespace Eshop.Catalog.Api.Features.DeleteProduct;

public sealed class DeleteProductCommandResult
{
    public DeleteProductCommandResult(bool isSucceeded)
    {
        IsSucceeded = isSucceeded;
    }

    public readonly bool IsSucceeded;
}
