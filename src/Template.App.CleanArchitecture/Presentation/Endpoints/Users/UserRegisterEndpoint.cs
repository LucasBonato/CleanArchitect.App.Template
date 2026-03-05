using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Users.Register;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Users;

internal sealed class UserRegisterEndpoint : IEndpoint
{
    private sealed record UserRegisterRequest(
        string Email,
        string FirstName,
        string LastName,
        string Password
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (
            [FromBody] UserRegisterRequest request,
            [FromServices] ICommandHandler<RegisterUserCommand, Guid> handler,
            CancellationToken cancellationToken
        ) => {
            RegisterUserCommand command = new(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .Produces<Guid>()
        .ProducesValidationProblem();
    }
}
