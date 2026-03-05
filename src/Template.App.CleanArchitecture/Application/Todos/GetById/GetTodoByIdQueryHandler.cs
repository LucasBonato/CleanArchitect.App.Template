using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Todos;

namespace Template.App.CleanArchitecture.Application.Todos.GetById;

internal sealed class GetTodoByIdQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext
) : IQueryHandler<GetTodoByIdQuery, GetTodoResponse> {
    public async Task<Result<GetTodoResponse>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        GetTodoResponse? todo = await context.TodoItems
            .Where(todoItem => todoItem.Id == query.TodoItemId && todoItem.UserId == userContext.UserId)
            .Select(todoItem =>
                new GetTodoResponse(
                    Id: todoItem.Id,
                    UserId: todoItem.UserId,
                    Description: todoItem.Description,
                    DueDate: todoItem.DueDate,
                    Labels: todoItem.Labels,
                    IsCompleted: todoItem.IsCompleted,
                    CreatedAt: todoItem.CreatedAt,
                    CompletedAt: todoItem.CompletedAt
                )
            )
            .SingleOrDefaultAsync(cancellationToken);

        return todo?? Result.Failure<GetTodoResponse>(TodoItemErrors.NotFound(query.TodoItemId));
    }
}
