using App.Application.Abstractions.Messaging;

namespace App.Application.Todos.Get;

public sealed record GetTodosQuery(Guid UserId) : IQuery<List<TodoResponse>>;
