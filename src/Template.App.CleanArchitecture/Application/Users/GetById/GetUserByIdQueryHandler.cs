using Microsoft.EntityFrameworkCore;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Application.Abstractions.Data;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Domain;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Application.Users.GetById;

internal sealed class GetUserByIdQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext
) : IQueryHandler<GetUserByIdQuery, GetUserResponse> {
    public async Task<Result<GetUserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
            return Result.Failure<GetUserResponse>(UserErrors.Unauthorized());

        GetUserResponse? user = await context.Users
            .Where(user => user.Id == query.UserId)
            .Select(user =>
                new GetUserResponse(
                    Id: user.Id,
                    Email: user.Email,
                    FirstName: user.FirstName,
                    LastName: user.LastName
                )
            )
            .SingleOrDefaultAsync(cancellationToken);

        return user ?? Result.Failure<GetUserResponse>(UserErrors.NotFound(query.UserId));
    }
}
