using Catalog.Api.Models;
using Eshop.Common.Cqrs;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public GetProductsQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    private readonly IDocumentSession _documentSession;

    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _documentSession
            .Query<Product>()
            .ToListAsync(cancellationToken);

        return new(products);
    }
}
