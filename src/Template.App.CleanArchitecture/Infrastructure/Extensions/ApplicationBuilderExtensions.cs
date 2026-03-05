using Scalar.AspNetCore;

namespace Template.App.CleanArchitecture.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseScalarWithOpenApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
        return app;
    }
}
