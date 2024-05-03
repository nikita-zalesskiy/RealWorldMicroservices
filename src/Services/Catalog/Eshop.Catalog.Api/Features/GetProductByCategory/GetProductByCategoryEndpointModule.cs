using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.GetProductByCategory;

public sealed class GetProductByCategoryEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", GetProductByCategory)
            .Produces<GetProductByCategoryQueryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Product By Category")
            .WithSummary("Get Product By Category")
            .WithName("GetProductByCategory");
    }

    private async Task<IResult> GetProductByCategory(string category, [FromServices] ISender sender)
    {
        var response = await sender.Send(new GetProductByCategoryQuery(category));

        return Results.Ok(response);
    }
}
