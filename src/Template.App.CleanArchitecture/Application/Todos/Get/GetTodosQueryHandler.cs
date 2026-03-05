using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Todos.Get;

internal sealed class GetTodosQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext
) : IQueryHandler<GetTodosQuery, List<GetTodoResponse>> {
    public async Task<Result<List<GetTodoResponse>>> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
            return Result.Failure<List<GetTodoResponse>>(UserErrors.Unauthorized());

        List<GetTodoResponse> todos = await context.TodoItems
            .Where(todoItem => todoItem.UserId == query.UserId)
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
            .ToListAsync(cancellationToken);

        return todos;
    }
}
