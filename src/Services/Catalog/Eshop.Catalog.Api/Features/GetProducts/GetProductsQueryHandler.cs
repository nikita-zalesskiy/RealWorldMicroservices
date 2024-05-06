using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public GetProductsQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    private readonly IDocumentSession _documentSession;

    public async Task<RequestResult<GetProductsQueryResult>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _documentSession
            .Query<Product>()
            .ToListAsync(cancellationToken);

        return new GetProductsQueryResult(products)
            .AsRequestResult();
    }
}
