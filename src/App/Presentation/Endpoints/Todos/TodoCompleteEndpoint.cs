using App.Application.Abstractions.Messaging;
using App.Application.Todos.Complete;
using App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Endpoints.Todos;

internal sealed class TodoCompleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("todos/{id:guid}/complete", async (
            [FromRoute] Guid id,
            [FromServices] ICommandHandler<CompleteTodoCommand> handler,
            CancellationToken cancellationToken
        ) => {
            CompleteTodoCommand command = new(id);

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
