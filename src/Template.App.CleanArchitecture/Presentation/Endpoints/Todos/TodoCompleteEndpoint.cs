using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Todos.Complete;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Todos;

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
