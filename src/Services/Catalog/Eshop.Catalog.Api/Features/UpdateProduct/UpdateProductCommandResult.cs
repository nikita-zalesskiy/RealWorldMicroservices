namespace Eshop.Catalog.Api.Features.UpdateProduct;

public sealed class UpdateProductCommandResult
{
    public UpdateProductCommandResult(bool isSucceeded)
    {
        IsSucceeded = isSucceeded;
    }

    public readonly bool IsSucceeded;
}
