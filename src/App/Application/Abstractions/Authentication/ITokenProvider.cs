using App.Domain.Users;

namespace App.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
