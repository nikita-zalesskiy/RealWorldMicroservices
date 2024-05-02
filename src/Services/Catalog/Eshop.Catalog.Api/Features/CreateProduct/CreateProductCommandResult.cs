using System.Diagnostics.CodeAnalysis;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public sealed class CreateProductCommandResult
{
    [SetsRequiredMembers]
    public CreateProductCommandResult(Guid productId)
    {
        ProductId = productId;
    }

    public required Guid ProductId { get; init; }

}
