using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider
) : ICommandHandler<LoginUserCommand, string> {
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == command.Email, cancellationToken);

        if (user is null)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        string token = tokenProvider.Create(user);

        return token;
    }
}
