using Eshop.Common.Web.Validation;
using FluentValidation;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

public sealed class UpdateProductCommandValidator : FunctionalValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty()
            .WithMessage("Product Id is required.");

        RuleForOption(command => command.Name, ruleBuilder
            => ruleBuilder.Length(min: 2, max: 150)
            .WithMessage("Name must be between 2 and 150 characters."));

        RuleForOption(command => command.Price, ruleBuilder
            => ruleBuilder.GreaterThan(decimal.Zero)
            .WithMessage("Price must be greater than zero."));
    }
}
