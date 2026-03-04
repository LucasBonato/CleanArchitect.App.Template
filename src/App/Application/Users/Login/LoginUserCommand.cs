using App.Application.Abstractions.Messaging;

namespace App.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
