using Eshop.Common.Cqrs;
using Optional;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

public sealed class UpdateProductCommand : ICommand<UpdateProductCommandResult>
{
    public Guid ProductId;

    public Option<string> Name;

    public Option<decimal> Price;

    public Option<string> ImageFile;

    public Option<string?> Description;

    public Option<List<string>> Categories;
}
