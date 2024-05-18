using Eshop.Common.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.GetProducts;

public sealed class GetProductsQuery : IQuery<GetProductsQueryResult>
{
    [FromQuery(Name = "page_size")]
    public int? PageSize { get; set; }

    [FromQuery(Name = "page_number")]
    public int? PageNumber { get; set; }

    public void Deconstruct(out int pageNumber, out int pageSize)
    {
        pageNumber = PageNumber.GetValueOrDefault(1);

        pageSize = PageSize.GetValueOrDefault(10);
    }
}
