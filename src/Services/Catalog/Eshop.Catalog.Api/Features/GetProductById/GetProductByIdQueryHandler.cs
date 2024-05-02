using Catalog.Api.Models;
using Eshop.Common.Cqrs;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public GetProductByIdQueryHandler(
        IDocumentSession documentSession
        , ILogger<GetProductByIdQueryHandler> logger)
    {
        _logger = logger;

        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;
    private readonly ILogger<GetProductByIdQueryHandler> _logger;

    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{ nameof(GetProductByIdQueryHandler) }.{ nameof(Handle) } called with {{@Query}}", query);

        var product = await _documentSession.LoadAsync<Product>(query.ProductId, cancellationToken);

        return new(product);

    }
}
