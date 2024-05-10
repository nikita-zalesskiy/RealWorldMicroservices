using Eshop.Common.Expressions;
using FluentValidation;
using Optional;
using Optional.Unsafe;
using System.Linq.Expressions;

namespace Eshop.Common.Web.Validation;

public class FunctionalValidator<TSource> : AbstractValidator<TSource>
{
    public void RuleForOption<TProperty>(
        Expression<Func<TSource, Option<TProperty>>> optionExpression
        , Action<IRuleBuilderInitial<TSource, TProperty>> action)
    {
        ArgumentNullException.ThrowIfNull(optionExpression);

        var propertyExpression = ExpressionTransformer.Combine(optionExpression, option => option.ValueOrDefault());

        var hasValueFunction = ExpressionTransformer
            .Combine(optionExpression, option => option.HasValue)
            .Compile();

        When(hasValueFunction, () =>
        {
            var ruleBuilderInitial = RuleFor(propertyExpression);

            action(ruleBuilderInitial);
        });
    }
}
