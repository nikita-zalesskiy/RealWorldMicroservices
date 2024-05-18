using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;
using Marten.Pagination;

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
        var (pageNumber, pageSize) = query;

        var products = await _documentSession.Query<Product>()
            .ToPagedListAsync(pageNumber, pageSize, cancellationToken);

        var queryResult = new GetProductsQueryResult(products);

        return queryResult.AsRequestResult();
    }
}
