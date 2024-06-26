﻿using Eshop.Common.Web.Functional;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.DeleteProduct;

public sealed class DeleteProductEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId}", DeleteProduct)
            .Produces<DeleteProductCommandResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }

    private async Task<IResult> DeleteProduct(Guid productId, [FromServices] ISender sender)
    {
        var requestResult = await sender.Send(new DeleteProductCommand(productId));

        return requestResult.ToHttpResult(Results.Ok);
    }
}
