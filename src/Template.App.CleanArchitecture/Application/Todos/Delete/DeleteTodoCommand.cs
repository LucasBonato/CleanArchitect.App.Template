using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Todos.Delete;

public sealed record DeleteTodoCommand(Guid TodoItemId) : ICommand;
