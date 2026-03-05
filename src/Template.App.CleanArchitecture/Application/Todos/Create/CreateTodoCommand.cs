using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain.Todos;

namespace Template.App.CleanArchitecture.Application.Todos.Create;

public sealed record CreateTodoCommand(
    Guid UserId,
    string Description,
    DateTime? DueDate,
    List<string> Labels,
    Priority Priority
) : ICommand<Guid>;
