using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Eshop.Common.Web;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, RequestResult<TResponse>>
    where TRequest : IRequest<TResponse>
{
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public async Task<RequestResult<TResponse>> Handle(TRequest request
        , RequestHandlerDelegate<RequestResult<TResponse>> next, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<TRequest>(request);

        var validationTasks = _validators.Select(validator => validator.ValidateAsync(validationContext, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks);

        var validationErrors = validationResults
            .SelectMany(validationResult => validationResult.Errors)
            .Select(Convert)
            .ToArray();

        if (validationErrors.Length > 0)
        {
            return validationErrors.AsRequestResultErrors<TResponse>();
        }

        return await next();
    }

    private static ValidationError Convert(ValidationFailure validationFailure)
    {
        return new(validationFailure.ErrorMessage);
    }
}
