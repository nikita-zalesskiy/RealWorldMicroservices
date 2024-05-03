using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.GetProductByCategory;

public sealed class GetProductByCategoryQuery : IQuery<GetProductByCategoryQueryResult>
{
    public GetProductByCategoryQuery(string category)
    {
        Category = category;
    }

    public readonly string Category;
}
