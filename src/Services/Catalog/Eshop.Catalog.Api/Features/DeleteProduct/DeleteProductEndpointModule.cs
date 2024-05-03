using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.DeleteProduct;

public sealed class DeleteProductEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId}", DeleteProduct)
            .Produces<DeleteProductCommandResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Delete Product")
            .WithSummary("Delete Product")
            .WithName("DeleteProduct");
    }

    private async Task<IResult> DeleteProduct(Guid productId, [FromServices] ISender sender)
    {
        var response = await sender.Send(new DeleteProductCommand(productId));

        return Results.Ok(response);
    }
}
