using JwtStore.Core;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtStore.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData data)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials,
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public static ClaimsIdentity GenerateClaims(ResponseData user)
    {
        var claimIdentity = new ClaimsIdentity();
        claimIdentity.AddClaim(new Claim("Id", user.Id));
        claimIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));

        foreach (var role in user.Roles)
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

        return claimIdentity;
    }
}
