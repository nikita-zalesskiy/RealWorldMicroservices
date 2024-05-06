using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public UpdateProductCommandHandler(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public async Task<RequestResult<UpdateProductCommandResult>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(command.ProductId, cancellationToken);

        if (product is null)
        {
            return new UpdateProductCommandResult(isSucceeded: false)
                .AsRequestResult();
        }

        if (command.Name is not null)
        {
            product.Name = command.Name;
        }

        if (command.Price is not null)
        {
            product.Price = command.Price.Value;
        }

        if (command.Categories is not null)
        {
            product.ReplaceCategories(command.Categories);
        }

        product.ImageFile = command.ImageFile;
        product.Description = command.Description;

        _documentSession.Update(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResult(isSucceeded: true)
            .AsRequestResult();
    }
}
