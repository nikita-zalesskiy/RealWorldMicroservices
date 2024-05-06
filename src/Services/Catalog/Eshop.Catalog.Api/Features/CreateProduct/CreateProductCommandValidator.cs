using FluentValidation;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(command => command.Categories)
            .NotEmpty()
            .WithMessage("Categories are required.");

        RuleFor(command => command.ImageFile)
            .NotEmpty()
            .WithMessage("ImageFile is required.");

        RuleFor(command => command.Price)
            .GreaterThan(decimal.Zero)
            .WithMessage("Price must be greater than zero.");
    }
}
