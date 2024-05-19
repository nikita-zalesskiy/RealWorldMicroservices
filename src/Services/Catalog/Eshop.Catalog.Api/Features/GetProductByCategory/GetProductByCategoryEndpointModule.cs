using Eshop.Common.Web.Functional;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.GetProductByCategory;

public sealed class GetProductByCategoryEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", GetProductByCategory)
            .Produces<GetProductByCategoryQueryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> GetProductByCategory(string category, [FromServices] ISender sender)
    {
        var query = new GetProductByCategoryQuery(category);

        var requestResult = await sender.Send(query);

        return requestResult.ToHttpResult(Results.Ok);
    }
}
