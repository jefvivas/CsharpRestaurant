using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Controllers;
[Route("/Auth")]
[ApiController]
public class Auth : ControllerBase
{

    [HttpPost]

    public IActionResult Post([FromBody] UserCredentials credentials)
    {
        if (credentials.Username == "usuario" && credentials.Password == "senha")
        {
            var token = GenerateJwtToken(credentials.Username);

            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        var key = Encoding.UTF8.GetBytes("08D856F45E32C98D0AA162BBD99E99D5");

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
