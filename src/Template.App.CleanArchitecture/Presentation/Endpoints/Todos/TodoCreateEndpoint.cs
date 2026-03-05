using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Todos.Create;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Todos;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Todos;

internal sealed class TodoCreateEndpoint : IEndpoint
{
    private sealed record TodoCreateRequest(
        Guid UserId,
        string Description,
        DateTime? DueDate,
        int Priority,
        List<string> Labels
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("todos", async (
            [FromBody] TodoCreateRequest request,
            [FromServices] ICommandHandler<CreateTodoCommand, Guid> handler,
            CancellationToken cancellationToken
        ) => {
            CreateTodoCommand command = new(
                UserId: request.UserId,
                Description: request.Description,
                DueDate: request.DueDate,
                Labels: request.Labels,
                Priority: (Priority)request.Priority
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .RequireAuthorization()
        .Produces<Guid>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
