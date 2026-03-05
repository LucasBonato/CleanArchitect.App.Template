using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Todos;

namespace Template.App.CleanArchitecture.Application.Todos.Delete;

internal sealed class DeleteTodoCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext
) : ICommandHandler<DeleteTodoCommand> {
    public async Task<Result> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await context.TodoItems
            .SingleOrDefaultAsync(todo => todo.Id == command.TodoItemId && todo.UserId == userContext.UserId, cancellationToken);

        if (todoItem is null)
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));

        context.TodoItems.Remove(todoItem);

        todoItem.Raise(new TodoItemDeletedDomainEvent(todoItem.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
