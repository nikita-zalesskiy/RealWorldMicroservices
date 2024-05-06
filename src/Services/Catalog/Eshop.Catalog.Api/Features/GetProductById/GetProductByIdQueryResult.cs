using Catalog.Api.Domain;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdQueryResult
{
    public GetProductByIdQueryResult(Product? product)
    {
        Product = product;
    }

    public readonly Product? Product;
}
