using Microsoft.Extensions.Primitives;

namespace App.Infrastructure.Middleware;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderName = "Correlation-Id";

    public async Task Invoke(HttpContext context, ILogger<RequestContextLoggingMiddleware> logger)
    {
        string correlationId = GetCorrelationId(context);

        using (logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
            await next(context);
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            CorrelationIdHeaderName,
            out StringValues correlationId
        );

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
