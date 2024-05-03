using Catalog.Api.Models;
using Eshop.Common.Cqrs;
using Marten;

namespace Eshop.Catalog.Api.Features.DeleteProduct;

internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public DeleteProductCommandHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        _documentSession.Delete<Product>(command.ProductId);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new(isSucceeded: true);
    }
}
