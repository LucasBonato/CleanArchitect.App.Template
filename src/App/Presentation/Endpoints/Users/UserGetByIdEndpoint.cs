using App.Application.Abstractions.Messaging;
using App.Application.Users.GetById;
using App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Endpoints.Users;

internal sealed class UserGetByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}", async (
            [FromRoute] Guid userId,
            [FromServices] IQueryHandler<GetUserByIdQuery, UserResponse> handler,
            CancellationToken cancellationToken
        ) => {
            GetUserByIdQuery query = new(userId);

            Result<UserResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UsersAccess)
        .WithTags(Tags.Users)
        .Produces<UserResponse>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
