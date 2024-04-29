using Eshop.Common.Cqrs;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public sealed record CreateProductCommand : ICommand<CreateProductCommandResult>
{

}
