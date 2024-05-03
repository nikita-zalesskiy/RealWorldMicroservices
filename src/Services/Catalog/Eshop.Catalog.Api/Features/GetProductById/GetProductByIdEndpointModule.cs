using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{productId}", GetProductById)
            .Produces<GetProductByIdQueryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Product By Id")
            .WithSummary("Get Product By Id")
            .WithName("GetProductById");
    }

    private async Task<IResult> GetProductById(Guid productId, [FromServices] ISender sender)
    {
        var response = await sender.Send(new GetProductByIdQuery(productId));

        return Results.Ok(response);
    }
}
