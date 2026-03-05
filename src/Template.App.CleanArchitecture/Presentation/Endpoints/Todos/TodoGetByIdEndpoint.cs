using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Todos;
using Template.App.CleanArchitecture.Application.Todos.GetById;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Todos;

internal sealed class TodoGetByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("todos/{id:guid}", async (
            [FromRoute] Guid id,
            [FromServices] IQueryHandler<GetTodoByIdQuery, GetTodoResponse> handler,
            CancellationToken cancellationToken
        ) => {
            GetTodoByIdQuery command = new(id);

            Result<GetTodoResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .RequireAuthorization()
        .Produces<GetTodoResponse>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
