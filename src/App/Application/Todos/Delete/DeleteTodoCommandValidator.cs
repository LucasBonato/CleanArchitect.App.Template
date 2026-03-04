using FluentValidation;

namespace App.Application.Todos.Delete;

internal sealed class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(todoCommand => todoCommand.TodoItemId).NotEmpty();
    }
}
