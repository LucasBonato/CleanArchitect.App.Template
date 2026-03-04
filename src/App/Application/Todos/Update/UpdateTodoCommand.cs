using App.Application.Abstractions.Messaging;

namespace App.Application.Todos.Update;

public sealed record UpdateTodoCommand(
    Guid TodoItemId,
    string Description
) : ICommand;
