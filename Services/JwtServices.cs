using Microsoft.IdentityModel.Tokens;
using Restaurant.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Services;

public class JwtServices : IJwtServices
{
    public string? GetUniqueNameClaim(HttpContext httpContext)
    {
        var authHeader = httpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authHeader))
        {
            return null;
        }

        var tokenParts = authHeader.Split(' ');

        if (tokenParts.Length != 2 || tokenParts[0] != "Bearer")
        {
            return null;
        }

        var token = tokenParts[1];
        var handler = new JwtSecurityTokenHandler();
        var tokens = handler.ReadJwtToken(token);

        return tokens.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
    }

    public string GenerateJwtToken(string username, string secretKey)
    {
        var key = Encoding.UTF8.GetBytes(secretKey);


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
