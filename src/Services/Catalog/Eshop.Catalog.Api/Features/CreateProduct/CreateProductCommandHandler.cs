using Catalog.Api.Models;
using Eshop.Common.Cqrs;
using Mapster;
using Marten;

namespace Eshop.Catalog.Api.Features.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public CreateProductCommandHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
    
    private readonly IDocumentSession _documentSession;

    public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = request.Adapt<Product>();

        _documentSession.Store(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new(product.ProductId);
    }
}
