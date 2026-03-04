using App.Application.Abstractions.Authentication;
using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain;
using App.Domain.Todos;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Todos.Complete;

internal sealed class CompleteTodoCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext
) : ICommandHandler<CompleteTodoCommand> {
    public async Task<Result> Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await context.TodoItems
            .SingleOrDefaultAsync(todo => todo.Id == command.TodoItemId && todo.UserId == userContext.UserId, cancellationToken);

        if (todoItem is null)
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));

        if (todoItem.IsCompleted)
            return Result.Failure(TodoItemErrors.AlreadyCompleted(command.TodoItemId));

        todoItem.IsCompleted = true;
        todoItem.CompletedAt = dateTimeProvider.UtcNow;

        todoItem.Raise(new TodoItemCompletedDomainEvent(todoItem.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
