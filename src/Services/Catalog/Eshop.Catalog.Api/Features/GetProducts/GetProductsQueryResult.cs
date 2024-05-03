using Catalog.Api.Models;

namespace Eshop.Catalog.Api.Features.GetProducts;

public sealed class GetProductsQueryResult
{
    public GetProductsQueryResult(IEnumerable<Product> products)
    {
        Products = products;
    }

    public readonly IEnumerable<Product> Products;
}
