using FluentValidation;

namespace Template.App.CleanArchitecture.Application.Todos.Complete;

internal sealed class CompleteTodoCommandValidator : AbstractValidator<CompleteTodoCommand>
{
    public CompleteTodoCommandValidator()
    {
        RuleFor(todoCommand => todoCommand.TodoItemId).NotEmpty();
    }
}
