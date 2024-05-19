using Carter;
using Eshop.Common.Web.Functional;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Basket.Api.Features.GetShoppingCart;

public sealed class GetShoppingCartEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shopping-carts/{userId}", GetShoppingCart)
            .Produces<GetShoppingCartQueryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> GetShoppingCart(Guid userId, [FromServices] ISender sender)
    {
        var query = new GetShoppingCartQuery(userId);

        var requestResult = await sender.Send(query);

        return requestResult.ToHttpResult(Results.Ok);
    }
}
