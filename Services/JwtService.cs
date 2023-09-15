using System.IdentityModel.Tokens.Jwt;

namespace Restaurant.Services;

public class JwtService
{
    public string GetUniqueNameClaim(HttpContext httpContext)
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
}
