using Eshop.Basket.Api.Persistence;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;

namespace Eshop.Basket.Api.Features.DeleteShoppingCart;

internal sealed class DeleteShoppingCartCommandHandler : ICommandHandler<DeleteShoppingCartCommand, DeleteShoppingCartCommandResult>
{
    public DeleteShoppingCartCommandHandler(RedisShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }
    
    private readonly RedisShoppingCartRepository _shoppingCartRepository;

    public async Task<RequestResult<DeleteShoppingCartCommandResult>> Handle(DeleteShoppingCartCommand command, CancellationToken cancellationToken)
    {
        await _shoppingCartRepository.DeleteShoppingCartAsync(command.UserId, cancellationToken);

        var commandResult = new DeleteShoppingCartCommandResult(isSucceeded: true);

        return commandResult.AsRequestResult();
    }
}
