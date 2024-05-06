using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public GetProductByIdQueryHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public async Task<RequestResult<GetProductByIdQueryResult>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(query.ProductId, cancellationToken);

        return new GetProductByIdQueryResult(product)
            .AsRequestResult();
    }
}
