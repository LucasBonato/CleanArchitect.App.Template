using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Users;
using Template.App.CleanArchitecture.Application.Users.GetById;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Users;

internal sealed class UserGetByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}", async (
            [FromRoute] Guid userId,
            [FromServices] IQueryHandler<GetUserByIdQuery, GetUserResponse> handler,
            CancellationToken cancellationToken
        ) => {
            GetUserByIdQuery query = new(userId);

            Result<GetUserResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UsersAccess)
        .WithTags(Tags.Users)
        .Produces<GetUserResponse>()
        .ProducesValidationProblem()
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
