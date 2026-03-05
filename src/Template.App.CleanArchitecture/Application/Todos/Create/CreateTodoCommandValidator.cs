using FluentValidation;

namespace Template.App.CleanArchitecture.Application.Todos.Create;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(todoCommand => todoCommand.UserId).NotEmpty();
        RuleFor(todoCommand => todoCommand.Priority).IsInEnum();
        RuleFor(todoCommand => todoCommand.Description).NotEmpty().MaximumLength(255);
        RuleFor(todoCommand => todoCommand.DueDate).GreaterThanOrEqualTo(DateTime.Today).When(todoCommand => todoCommand.DueDate.HasValue);
    }
}
