using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Template.App.CleanArchitecture.Application.Abstractions.Authentication;
using Template.App.CleanArchitecture.Domain.Users;

namespace Template.App.CleanArchitecture.Infrastructure.Authentication;

internal sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string Create(User user)
    {
        string secretKey = configuration["Jwt:Secret"]!;
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));

        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor tokenDescriptor = new() {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        JsonWebTokenHandler handler = new();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
