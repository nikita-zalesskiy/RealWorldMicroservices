using Eshop.Common.Cqrs;

namespace Eshop.Basket.Api.Features.DeleteShoppingCart;

public sealed class DeleteShoppingCartCommand : ICommand<DeleteShoppingCartCommandResult>
{
    public DeleteShoppingCartCommand(Guid userId)
    {
        UserId = userId;
    }

    public readonly Guid UserId;
}
