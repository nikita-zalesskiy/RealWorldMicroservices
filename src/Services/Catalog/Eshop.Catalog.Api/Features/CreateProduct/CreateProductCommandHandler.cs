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

    public async Task<CreateProductCommandResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = command.Adapt<Product>();

        _documentSession.Store(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new(product.ProductId);
    }
}
