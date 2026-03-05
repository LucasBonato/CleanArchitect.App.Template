using Microsoft.AspNetCore.Mvc;
using Template.App.CleanArchitecture.Application.Abstractions.Messaging;
using Template.App.CleanArchitecture.Application.Users.Login;
using Template.App.CleanArchitecture.Domain;

namespace Template.App.CleanArchitecture.Presentation.Endpoints.Users;

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
