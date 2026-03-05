using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Todos;

namespace Template.App.CleanArchitecture.Application.Todos.Update;

internal sealed class UpdateTodoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<UpdateTodoCommand> {
    public async Task<Result> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await context.TodoItems
            .SingleOrDefaultAsync(todo => todo.Id == command.TodoItemId, cancellationToken);

        if (todoItem is null)
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));

        todoItem.Description = command.Description;

        await context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
