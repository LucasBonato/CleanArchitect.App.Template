using System.Reflection;
using App.Infrastructure;
using App.Infrastructure.Extensions;
using App.Presentation.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApiWithAuth()
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseScalarWithOpenApi();
    app.ApplyMigrations();
}

app.UseRequestContextLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();
