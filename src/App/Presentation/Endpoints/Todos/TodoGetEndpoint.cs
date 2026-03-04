using App.Application.Abstractions.Messaging;
using App.Application.Todos.Get;
using App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Endpoints.Todos;

internal sealed class TodoGetEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("todos", async (
            [FromQuery] Guid userId,
            [FromServices] IQueryHandler<GetTodosQuery, List<TodoResponse>> handler,
            CancellationToken cancellationToken
        ) => {
            GetTodosQuery query = new(userId);

            Result<List<TodoResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .RequireAuthorization()
        .Produces<List<TodoResponse>>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
