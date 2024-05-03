using Catalog.Api.Models;
using Eshop.Common.Cqrs;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public GetProductByIdQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(query.ProductId, cancellationToken);

        return new(product);

    }
}
