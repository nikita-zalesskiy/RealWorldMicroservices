using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Eshop.Common.Web.Functional;
using Eshop.Basket.Api.Domain;

namespace Eshop.Basket.Api.Features.UpsertShoppingCart;

public sealed class UpsertShoppingCartEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/shopping-carts", UpsertShoppingCart)
            .Produces<UpsertShoppingCartCommandResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> UpsertShoppingCart(ShoppingCart shoppingCart, [FromServices] ISender sender)
    {
        var command = new UpsertShoppingCartCommand(shoppingCart);

        var requestResult = await sender.Send(command);

        return requestResult.ToHttpResult(GetUpsertResponse);
    }

    private static IResult GetUpsertResponse(UpsertShoppingCartCommandResult commandResult)
    {
        return Results.Created($"/products/{ commandResult.UserId }", commandResult);
    }
}
