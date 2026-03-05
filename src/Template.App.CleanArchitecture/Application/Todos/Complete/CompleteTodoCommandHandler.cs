using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Todos;

namespace Template.App.CleanArchitecture.Application.Todos.Complete;

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
