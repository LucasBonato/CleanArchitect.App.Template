using App.Application.Abstractions.Authentication;
using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Users.GetById;

internal sealed class GetUserByIdQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext
) : IQueryHandler<GetUserByIdQuery, UserResponse> {
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());

        UserResponse? user = await context.Users
            .Where(user => user.Id == query.UserId)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            })
            .SingleOrDefaultAsync(cancellationToken);

        return user ?? Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
    }
}
