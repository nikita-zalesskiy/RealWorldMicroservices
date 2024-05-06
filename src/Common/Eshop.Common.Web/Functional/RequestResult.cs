using Eshop.Common.Web.Validation;
using Optional;
using Optional.Unsafe;
using System.Diagnostics.CodeAnalysis;

namespace Eshop.Common.Web.Functional;

public sealed class RequestResult<TResult>
{
    internal RequestResult(TResult result)
    {
        _result = Option.Some<TResult, ValidationErrorCollection>(result);
    }

    internal RequestResult(ValidationErrorCollection validationErrors)
    {
        _result = Option.None<TResult, ValidationErrorCollection>(validationErrors);
    }

    private readonly Option<TResult, ValidationErrorCollection> _result;

    public bool IsSucceeded => _result.HasValue;

    public bool TryGetResult([NotNullWhen(returnValue: true)] out TResult? result)
    {
        result = _result.ValueOrDefault();

        return IsSucceeded;
    }

    public TDestination Match<TDestination>(
        Func<TResult, TDestination> resultMapping
        , Func<ValidationErrorCollection, TDestination> errorsMapping)
    {
        return _result.Match(resultMapping, errorsMapping);
    }

    public static RequestResult<TResult> FromResult(TResult result)
    {
        return new(result);
    }

    public static RequestResult<TResult> FromErrors(ValidationErrorCollection validationErrors)
    {
        return new(validationErrors);
    }
}
