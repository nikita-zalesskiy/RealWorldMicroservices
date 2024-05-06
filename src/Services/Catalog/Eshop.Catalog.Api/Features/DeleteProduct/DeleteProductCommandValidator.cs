using FluentValidation;

namespace Eshop.Catalog.Api.Features.DeleteProduct;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty()
            .WithMessage("Product Id is required.");
    }
}
