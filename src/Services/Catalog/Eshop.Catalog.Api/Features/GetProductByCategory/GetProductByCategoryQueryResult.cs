﻿using Catalog.Api.Models;

namespace Eshop.Catalog.Api.Features.GetProductByCategory;

public sealed class GetProductByCategoryQueryResult
{
    public GetProductByCategoryQueryResult(IEnumerable<Product> products)
    {
        Products = products;
    }

    public readonly IEnumerable<Product> Products;
}
