using Carter;
using Eshop.Common.Web.Functional;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Basket.Api.Features.DeleteShoppingCart;

public sealed class DeleteShoppingCartEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/shopping-carts/{userId}", DeleteShoppingCart)
            .Produces<DeleteShoppingCartCommandResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> DeleteShoppingCart(Guid userId, [FromServices] ISender sender)
    {
        var command = new DeleteShoppingCartCommand(userId);

        var requestResult = await sender.Send(command);

        return requestResult.ToHttpResult(Results.Ok);
    }
}
