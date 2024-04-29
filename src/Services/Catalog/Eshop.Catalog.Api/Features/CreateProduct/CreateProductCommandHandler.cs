using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = Guid.NewGuid();

        return Task.FromResult<CreateProductCommandResult>(new(productId));
    }
}
