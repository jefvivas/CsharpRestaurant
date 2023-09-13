using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Restaurant.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Controllers;

[Route("/login")]
[ApiController]
public class TableLogin : ControllerBase
{
    private readonly IMongoCollection<Table> _collection;

    public TableLogin(IMongoCollection<Table> collection)
    {
        _collection = collection;
    }

    [HttpPost]

    public IActionResult Post([FromBody] Table credentials)
    {
        var tableFound = _collection.Find(t => t.Number == credentials.Number).FirstOrDefault();

        if (tableFound == null)
        {
            return Unauthorized("Number/password dont match");

        }
        if (BCrypt.Net.BCrypt.Verify(credentials.Password, tableFound.Password))
        {
            var token = GenerateJwtToken(credentials.Number);

            return Ok(new { Token = token });
        }

        return Unauthorized("Number/password dont match");
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
