using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Health;

internal sealed class HealthGetEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapHealthChecks("health", new HealthCheckOptions {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        })
        .WithTags(Tags.Health)
        .AllowAnonymous();
    }
}
