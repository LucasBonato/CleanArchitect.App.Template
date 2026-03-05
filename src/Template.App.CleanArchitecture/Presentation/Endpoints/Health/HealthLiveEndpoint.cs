using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Health;

internal sealed class HealthLiveEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapHealthChecks("/health/live", new HealthCheckOptions {
            Predicate = _ => false
        })
        .WithTags(Tags.Health)
        .AllowAnonymous();
    }
}
