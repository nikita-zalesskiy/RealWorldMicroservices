namespace Eshop.Catalog.Api.Features.CreateProduct;

public sealed class CreateProductCommandResult
{
    public CreateProductCommandResult(Guid productId)
    {
        ProductId = productId;
    }

    public readonly Guid ProductId;

}
