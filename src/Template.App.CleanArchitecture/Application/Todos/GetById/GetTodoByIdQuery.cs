using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Todos.GetById;

public sealed record GetTodoByIdQuery(Guid TodoItemId) : IQuery<GetTodoResponse>;
