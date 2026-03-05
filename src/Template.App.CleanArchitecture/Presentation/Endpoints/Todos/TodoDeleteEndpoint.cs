using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Todos.Delete;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Todos;

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
