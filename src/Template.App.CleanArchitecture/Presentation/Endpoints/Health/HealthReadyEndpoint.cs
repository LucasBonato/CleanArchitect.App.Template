using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Health;

internal sealed class HealthReadyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/health/ready", new HealthCheckOptions {
            Predicate = check => check.Tags.Contains("ready"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        })
        .WithTags(Tags.Health)
        .AllowAnonymous();
    }
}
