namespace Restaurant.Interfaces;

public interface IJwtServices
{
    string GetUniqueNameClaim(HttpContext httpContext);
    string GenerateJwtToken(string username, string secretKey);

}
