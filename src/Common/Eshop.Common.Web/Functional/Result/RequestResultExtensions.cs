using Microsoft.AspNetCore.Http;

namespace Eshop.Common.Web.Functional;

public static class RequestResultExtensions
{
    public static RequestResult<TResult> AsRequestResult<TResult>(this TResult result)
    {
        return RequestResult<TResult>.FromResult(result);
    }

    public static RequestResult<TResult> AsRequestResultErrors<TResult>(this IEnumerable<ValidationError> validationErrors)
    {
        return RequestResult<TResult>.FromErrors(new(validationErrors));
    }

    public static RequestResult<TResult> AsRequestResultErrors<TResult>(this ValidationErrorCollection validationErrors)
    {
        return RequestResult<TResult>.FromErrors(validationErrors);
    }

    public static IResult ToHttpResult<TResult>(this RequestResult<TResult> requestResult, Func<TResult, IResult> resultMapping)
    {
        return requestResult.Match(resultMapping, Results.BadRequest);
    }

}
