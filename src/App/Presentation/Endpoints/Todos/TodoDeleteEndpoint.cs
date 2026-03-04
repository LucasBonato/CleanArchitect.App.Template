using App.Application.Abstractions.Messaging;
using App.Application.Todos.Delete;
using App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Endpoints.Todos;

internal sealed class TodoDeleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("todos/{id:guid}", async (
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<DeleteTodoCommand> handler,
            CancellationToken cancellationToken
        ) => {
            DeleteTodoCommand command = new(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .RequireAuthorization()
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
