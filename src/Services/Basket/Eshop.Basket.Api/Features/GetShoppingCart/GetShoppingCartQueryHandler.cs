using Eshop.Basket.Api.Persistence;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;

namespace Eshop.Basket.Api.Features.GetShoppingCart;

internal sealed class GetShoppingCartQueryHandler : IQueryHandler<GetShoppingCartQuery, GetShoppingCartQueryResult>
{
    public GetShoppingCartQueryHandler(RedisShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    private readonly RedisShoppingCartRepository _shoppingCartRepository;

    public async Task<RequestResult<GetShoppingCartQueryResult>> Handle(GetShoppingCartQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await _shoppingCartRepository.GetShoppingCartAsync(query.UserId, cancellationToken);

        var queryResult = new GetShoppingCartQueryResult(shoppingCart);

        return queryResult.AsRequestResult();
    }
}
