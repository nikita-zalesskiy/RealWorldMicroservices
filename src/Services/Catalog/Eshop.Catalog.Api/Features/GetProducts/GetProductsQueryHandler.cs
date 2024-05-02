using Catalog.Api.Models;
using Eshop.Common.Cqrs;
using Marten;

namespace Eshop.Catalog.Api.Features.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsQueryResult>
{
    public GetProductsQueryHandler(
        IDocumentSession documentSession
        , ILogger<GetProductsQueryHandler> logger)
    {
        _logger = logger;

        _documentSession = documentSession;
    }
    
    private readonly IDocumentSession _documentSession;
    private readonly ILogger<GetProductsQueryHandler> _logger;

    public async Task<GetProductsQueryResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{ nameof(GetProductsQueryHandler) }.{ nameof(Handle) } called with {{@Query}}", query);

        var products = await _documentSession
            .Query<Product>()
            .ToListAsync(cancellationToken);

        return new(products);
    }
}
