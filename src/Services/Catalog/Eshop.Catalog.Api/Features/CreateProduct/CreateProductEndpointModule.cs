using Eshop.Common.Web.Functional;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public class CreateProductEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", CreateProduct)
            .Produces<CreateProductCommandResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> CreateProduct(CreateProductCommand command, [FromServices] ISender sender)
    {
        var requestResult = await sender.Send(command);

        return requestResult.ToHttpResult(GetCreateResponse);
    }

    private static IResult GetCreateResponse(CreateProductCommandResult commandResult)
    {
        return Results.Created($"/products/{ commandResult.ProductId }", commandResult);
    }
}
