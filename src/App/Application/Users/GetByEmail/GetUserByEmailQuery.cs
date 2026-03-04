using App.Application.Abstractions.Messaging;

namespace App.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
