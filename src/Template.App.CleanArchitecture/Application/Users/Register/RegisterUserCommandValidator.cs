using FluentValidation;

namespace Template.App.CleanArchitecture.Application.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(userCommand => userCommand.FirstName).NotEmpty();
        RuleFor(userCommand => userCommand.LastName).NotEmpty();
        RuleFor(userCommand => userCommand.Email).NotEmpty().EmailAddress();
        RuleFor(userCommand => userCommand.Password).NotEmpty().MinimumLength(8);
    }
}
