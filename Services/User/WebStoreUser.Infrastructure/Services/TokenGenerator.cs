using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebStoreUser.Application.Dtos;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Domain.Entities;

namespace WebStoreUser.Infrastructure.Services;

public class TokenGenerator(IConfiguration configuration) : ITokenGenerator
{
    public TokenResponseDto CreateTokens(User user)
    {
        return new TokenResponseDto(CreateAccessToken(user), CreateRefreshToken());
    }

    private string CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string CreateAccessToken(User user)
    {
        var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            claims: claims,
            issuer: configuration.GetValue<string>("AppSettings:Issuer")!,
            audience: configuration.GetValue<string>("AppSettings:Audience")!,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
