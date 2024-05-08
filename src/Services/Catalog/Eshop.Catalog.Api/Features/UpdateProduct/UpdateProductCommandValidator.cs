using Eshop.Common.Functional;
using FluentValidation;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty()
            .WithMessage("Product Id is required.");
        
        //RuleFor(command => command.Name)
        //    .Length(min: 2, max: 150)
        //    .WithMessage("Name must be between 2 and 150 characters.");

        //RuleFor(command => command.Price)
        //    .GreaterThan(decimal.Zero)
        //    .WithMessage("Price must be greater than zero.");
    }
}
