using Catalog.Api.Models;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdQueryResult
{
    public GetProductByIdQueryResult(Product? product)
    {
        Product = product;
    }
    
    public Product? Product { get; }

}
