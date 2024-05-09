using FluentValidation.Validators;
using FluentValidation;

namespace Eshop.Common.Web;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> Null<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new NullValidator<T, TProperty>());
    }
}
