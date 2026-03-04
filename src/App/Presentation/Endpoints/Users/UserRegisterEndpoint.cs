using App.Application.Abstractions.Messaging;
using App.Application.Users.Register;
using App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Endpoints.Users;

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
