﻿using Eshop.Catalog.Api.Features.CreateProduct;
using Eshop.Common.Web.Functional;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Catalog.Api.Features.GetProducts;

public sealed class GetProductEndpointModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", GetProducts)
            .Produces<GetProductsQueryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Products")
            .WithSummary("Get Products")
            .WithName("GetProducts");
    }

    private async Task<IResult> GetProducts([FromServices] ISender sender)
    {
        var requestResult = await sender.Send(new GetProductsQuery());

        return requestResult.ToHttpResult(Results.Ok);
    }
}