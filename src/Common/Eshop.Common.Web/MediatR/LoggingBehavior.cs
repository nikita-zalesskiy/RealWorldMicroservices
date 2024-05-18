using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Eshop.Common.Web;

public sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    private static readonly long s_loggingThreshold = 50;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        var response = await next();

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > s_loggingThreshold)
        {
            var requestType = typeof(TRequest);

            _logger.LogWarning("Request Type: { Request }; Processing Time (ms): { ProcessingTime }"
                , requestType.Name, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}
