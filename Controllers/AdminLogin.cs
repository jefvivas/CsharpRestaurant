using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Restaurant.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Controllers;

[Route("/admin/login")]
[ApiController]
public class AdminLogin : ControllerBase
{

    private readonly IMongoCollection<Admin> _collection;

    public AdminLogin(IMongoCollection<Admin> collection)
    {
        _collection = collection;
    }

    [HttpPost]

    public IActionResult Post([FromBody] Admin credentials)
    {
        var adminFound = _collection.Find(a => a.Username == credentials.Username).FirstOrDefault();


        if (adminFound == null)
        {
            return Unauthorized("Username/password dont match");

        }
        if (BCrypt.Net.BCrypt.Verify(credentials.Password, adminFound.Password))
        {
            var token = GenerateJwtToken(credentials.Username);

            return Ok(new { Token = token });
        }

        return Unauthorized("Username/passworde dont match");
    }

    private string GenerateJwtToken(string username)
    {
        var key = Encoding.UTF8.GetBytes("3A9F041FD4B9E0C12D0B8F008F5E1B76D8DCA1CEBB36E5E586A81D5B936F276");

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
