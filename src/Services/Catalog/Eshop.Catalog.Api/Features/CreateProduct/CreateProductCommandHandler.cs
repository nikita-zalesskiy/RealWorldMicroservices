using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
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

    public async Task<RequestResult<CreateProductCommandResult>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = command.Adapt<Product>();

        _documentSession.Store(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new CreateProductCommandResult(product.ProductId)
            .AsRequestResult();
    }
}
