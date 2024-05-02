using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public sealed record CreateProductCommand : ICommand<CreateProductCommandResult>
{
    public required string Name;

    public required decimal Price;

    public string? Description;

    public string? ImageFile;

    public List<string>? Categories = [];
}
