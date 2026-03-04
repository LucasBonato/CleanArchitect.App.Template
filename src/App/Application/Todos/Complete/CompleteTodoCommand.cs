using App.Application.Abstractions.Messaging;

namespace App.Application.Todos.Complete;

public sealed record CompleteTodoCommand(Guid TodoItemId) : ICommand;
