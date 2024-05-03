using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.DeleteProduct;

public sealed class DeleteProductCommand : ICommand<DeleteProductCommandResult>
{
    public DeleteProductCommand(Guid productId)
    {
        ProductId = productId;
    }
    
    public readonly Guid ProductId;
}
