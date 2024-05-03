using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public class CreateProductEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", CreateProduct)
            .Produces<CreateProductCommandResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Create Product")
            .WithSummary("Create Product")
            .WithName("CreateProduct");
    }

    private static async Task<IResult> CreateProduct(CreateProductCommand createProductCommand, [FromServices] ISender sender)
    {
        var response = await sender.Send(createProductCommand);

        return Results.Created($"/products/{ response.ProductId }", response);
    }
}
