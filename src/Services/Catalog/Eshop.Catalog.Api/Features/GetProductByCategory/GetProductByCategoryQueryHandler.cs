using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProductByCategory;

internal sealed class GetProductByCategoryQueryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryQueryResult>
{
    public GetProductByCategoryQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public async Task<RequestResult<GetProductByCategoryQueryResult>> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _documentSession.Query<Product>()
            .Where(product => product.Categories.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryQueryResult(products)
            .AsRequestResult();
    }
}
