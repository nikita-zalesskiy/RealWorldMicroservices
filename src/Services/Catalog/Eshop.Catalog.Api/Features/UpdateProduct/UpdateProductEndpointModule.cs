using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

public sealed class UpdateProductEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", UpdateProduct)
            .Produces<UpdateProductCommandResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Update Product")
            .WithSummary("Update Product")
            .WithName("UpdateProduct");
    }

    private async Task<IResult> UpdateProduct(UpdateProductCommand command, [FromServices] ISender sender)
    {
        var response = await sender.Send(command);

        return Results.Ok(response);
    }
}
