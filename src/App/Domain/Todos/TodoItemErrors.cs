namespace App.Domain.Todos;

public static class TodoItemErrors
{
    public static Error AlreadyCompleted(Guid todoItemId) => Error.Problem(
        code: "TodoItems.AlreadyCompleted",
        description: $"The todo item with Id = '{todoItemId}' is already completed."
    );

    public static Error NotFound(Guid todoItemId) => Error.NotFound(
        code: "TodoItems.NotFound",
        description: $"The to-do item with the Id = '{todoItemId}' was not found"
    );
}
