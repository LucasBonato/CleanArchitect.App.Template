using App.Application.Abstractions.Messaging;
using App.Application.Users.Login;
using App.Domain;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Endpoints.Users;

internal sealed class UserLoginEndpoint : IEndpoint
{
    private sealed record UserLoginRequest(
        string Email,
        string Password
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (
            [FromBody] UserLoginRequest request,
            [FromServices] ICommandHandler<LoginUserCommand, string> handler,
            CancellationToken cancellationToken
        ) => {
            LoginUserCommand command = new(request.Email, request.Password);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users)
        .ProducesValidationProblem();
    }
}
