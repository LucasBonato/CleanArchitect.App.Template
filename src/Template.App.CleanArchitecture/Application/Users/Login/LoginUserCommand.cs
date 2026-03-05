using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
