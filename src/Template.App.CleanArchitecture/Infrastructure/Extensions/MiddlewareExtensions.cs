using Template.App.CleanArchitecture.Infrastructure.Middleware;

namespace Template.App.CleanArchitecture.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestContextLoggingMiddleware>();
    }
}
