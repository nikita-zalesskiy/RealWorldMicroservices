using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

public sealed class UpdateProductCommand : ICommand<UpdateProductCommandResult>
{
    public Guid ProductId;

    public string? Name;

    public decimal? Price;

    public string? ImageFile;

    public string? Description;

    public List<string>? Categories;
}
