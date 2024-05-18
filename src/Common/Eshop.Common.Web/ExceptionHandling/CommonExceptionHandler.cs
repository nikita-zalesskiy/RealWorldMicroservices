using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Common.Web;

public sealed class CommonExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var request = httpContext.Request;

        var response = httpContext.Response;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError
            , Instance = request.Path
            , Extensions =
            {
                { "trace_id", httpContext.TraceIdentifier }
            }
        };

        await response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
