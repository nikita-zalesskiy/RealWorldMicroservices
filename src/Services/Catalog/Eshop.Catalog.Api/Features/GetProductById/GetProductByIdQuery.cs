using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdQuery : IQuery<GetProductByIdQueryResult>
{
    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }

    public readonly Guid ProductId;
}
