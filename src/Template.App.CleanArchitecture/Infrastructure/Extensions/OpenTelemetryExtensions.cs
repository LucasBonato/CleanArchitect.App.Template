using System.Diagnostics.Metrics;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Template.App.CleanArchitecture.Infrastructure.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetryConfiguration(this IServiceCollection services)
    {
        string serviceName = AppEnv.OTEL_SERVICE_NAME.NotNull();

        services
            .AddOpenTelemetry()
            .UseOtlpExporter()
            .ConfigureResource(resource =>
                resource.AddService(serviceName: serviceName)
            )
            .WithTracing(tracing =>
                tracing
                    .AddSource(serviceName)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsql()
            )
            .WithMetrics(metrics =>
                metrics
                    .AddMeter(serviceName)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsqlInstrumentation()
                    .AddView(instrument =>
                        instrument.GetType().GetGenericTypeDefinition() == typeof(Histogram<>)
                            ? new ExplicitBucketHistogramConfiguration()
                            : null
                    )
            )
            ;

        services.AddLogging(options =>
            options
                .AddOpenTelemetry(logger => {
                    logger.IncludeScopes = true;
                    logger.ParseStateValues = true;
                    logger.IncludeFormattedMessage = true;
                })
        );

        return services;
    }
}
