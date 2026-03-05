using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Todos.Get;

public sealed record GetTodosQuery(Guid UserId) : IQuery<List<GetTodoResponse>>;
