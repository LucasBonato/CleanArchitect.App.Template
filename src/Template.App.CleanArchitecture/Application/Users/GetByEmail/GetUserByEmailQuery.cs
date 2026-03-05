using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<GetUserResponse>;
