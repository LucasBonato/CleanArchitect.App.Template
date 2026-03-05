using Template.App.CleanArchitecture.Application.Abstractions.Authentication;

namespace Template.App.CleanArchitecture.Infrastructure.Authentication;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor
) : IUserContext {
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");
}
