using Eshop.Common.Web.Functional;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.GetProductById;

public sealed class GetProductByIdEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{productId}", GetProductById)
            .Produces<GetProductByIdQueryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> GetProductById(Guid productId, [FromServices] ISender sender)
    {
        var query = new GetProductByIdQuery(productId);

        var requestResult = await sender.Send(query);

        return requestResult.ToHttpResult(Results.Ok);
    }
}
