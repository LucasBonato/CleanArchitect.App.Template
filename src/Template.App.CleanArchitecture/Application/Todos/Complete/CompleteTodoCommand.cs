using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Todos.Complete;

public sealed record CompleteTodoCommand(Guid TodoItemId) : ICommand;
