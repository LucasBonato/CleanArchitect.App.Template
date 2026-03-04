using App.Application.Abstractions.Messaging;

namespace App.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
