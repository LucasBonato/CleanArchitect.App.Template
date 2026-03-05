using Microsoft.OpenApi;

namespace Template.App.CleanArchitecture.Infrastructure.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddOpenApiWithAuth(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
            options.AddDocumentTransformer((doc, _, _) => {
                doc.Components ??= new OpenApiComponents();
                doc.Security ??= [];
                doc.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                doc.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                doc.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer"),
                        []
                    }
                });

                return Task.CompletedTask;
            })
        );

        return services;
    }
}
