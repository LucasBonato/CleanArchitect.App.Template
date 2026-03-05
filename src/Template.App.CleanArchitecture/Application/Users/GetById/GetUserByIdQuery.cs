using Template.App.CleanArchitecture.Application.Abstractions.Messaging;

namespace Template.App.CleanArchitecture.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<GetUserResponse>;
