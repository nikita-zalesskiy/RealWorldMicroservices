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


        // AutoMapper.
        
        product.Name = command.Name.ValueOr(product.Name);

        product.Price = command.Price.ValueOr(product.Price);

        product.ImageFile = command.ImageFile.ValueOr(product.ImageFile);

        product.Description = command.Description.ValueOr(product.Description);

        product.Categories = command.Categories.ValueOr(product.Categories);

        _documentSession.Update(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResult(isSucceeded: true)
            .AsRequestResult();
    }
}
