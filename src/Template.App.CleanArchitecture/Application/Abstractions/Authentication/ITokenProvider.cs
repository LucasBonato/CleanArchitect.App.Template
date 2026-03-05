using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
