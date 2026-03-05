using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Todos.Update;

public sealed record UpdateTodoCommand(
    Guid TodoItemId,
    string Description
) : ICommand;
