using Eshop.Basket.Api.Persistence;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;

namespace Eshop.Basket.Api.Features.UpsertShoppingCart;

internal sealed class UpsertShoppingCartCommandHandler : ICommandHandler<UpsertShoppingCartCommand, UpsertShoppingCartCommandResult>
{
    public UpsertShoppingCartCommandHandler(RedisShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }
    
    private readonly RedisShoppingCartRepository _shoppingCartRepository;

    public async Task<RequestResult<UpsertShoppingCartCommandResult>> Handle(UpsertShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = command.ShoppingCart;

        await _shoppingCartRepository.UpsertShoppingCartAsync(shoppingCart, cancellationToken);

        var commandResult = new UpsertShoppingCartCommandResult(shoppingCart.UserId);

        return commandResult.AsRequestResult();
    }
}
