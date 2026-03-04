using App.Application.Abstractions.Messaging;

namespace App.Application.Todos.Delete;

public sealed record DeleteTodoCommand(Guid TodoItemId) : ICommand;
