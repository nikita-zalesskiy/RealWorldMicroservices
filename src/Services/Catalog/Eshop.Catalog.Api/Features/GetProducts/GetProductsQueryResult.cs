using Catalog.Api.Models;
using System.Diagnostics.CodeAnalysis;

namespace Eshop.Catalog.Api.Features.GetProducts;

public sealed class GetProductsQueryResult
{
    [SetsRequiredMembers]
    public GetProductsQueryResult(IEnumerable<Product> products)
    {
        Products = products;
    }

    public required IEnumerable<Product> Products { get; init; }
}
