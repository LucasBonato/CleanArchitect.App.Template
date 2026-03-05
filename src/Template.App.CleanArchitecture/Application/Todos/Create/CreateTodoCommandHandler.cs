using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Todos;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Todos.Create;

internal sealed class CreateTodoCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext
) : ICommandHandler<CreateTodoCommand, Guid> {
    public async Task<Result<Guid>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        if (userContext.UserId != command.UserId)
            return Result.Failure<Guid>(UserErrors.Unauthorized());

        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == command.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));

        TodoItem todoItem = new() {
            UserId = user.Id,
            Description = command.Description,
            Priority = command.Priority,
            DueDate = command.DueDate,
            Labels = command.Labels,
            IsCompleted = false,
            CreatedAt = dateTimeProvider.UtcNow
        };

        todoItem.Raise(new TodoItemCreatedDomainEvent(todoItem.Id));

        context.TodoItems.Add(todoItem);

        await context.SaveChangesAsync(cancellationToken);

        return todoItem.Id;
    }
}
