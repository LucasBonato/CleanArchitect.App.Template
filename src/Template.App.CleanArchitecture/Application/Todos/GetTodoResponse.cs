namespace Template.App.CleanArchitecture.Application.Todos;

public sealed record GetTodoResponse(
    Guid Id,
    Guid UserId,
    string Description,
    DateTime? DueDate,
    List<string> Labels,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime? CompletedAt
);
