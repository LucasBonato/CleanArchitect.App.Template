using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Users.GetByEmail;

internal sealed class GetUserByEmailQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext
) : IQueryHandler<GetUserByEmailQuery, GetUserResponse> {
    public async Task<Result<GetUserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        GetUserResponse? user = await context.Users
            .Where(user => user.Email == query.Email)
            .Select(user =>
                new GetUserResponse(
                    Id: user.Id,
                    Email: user.Email,
                    FirstName: user.FirstName,
                    LastName: user.LastName
                )
            )
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            return Result.Failure<GetUserResponse>(UserErrors.NotFoundByEmail);

        if (user.Id != userContext.UserId)
            return Result.Failure<GetUserResponse>(UserErrors.Unauthorized());

        return user;
    }
}
