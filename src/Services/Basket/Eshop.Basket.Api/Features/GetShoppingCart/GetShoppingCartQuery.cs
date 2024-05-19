using Eshop.Common.Cqrs;

namespace Eshop.Basket.Api.Features.GetShoppingCart;

public sealed class GetShoppingCartQuery : IQuery<GetShoppingCartQueryResult>
{
    public GetShoppingCartQuery(Guid userId)
    {
        UserId = userId;
    }
    
    public readonly Guid UserId;
}
