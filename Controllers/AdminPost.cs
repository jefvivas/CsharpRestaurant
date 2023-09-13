using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Restaurant.Models;
namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("admin")]
[ApiController]
public class AdminPost : ControllerBase
{
    private readonly IMongoCollection<Admin> _collection;

    public AdminPost(IMongoCollection<Admin> collection)
    {
        _collection = collection;
    }

    [HttpPost]

    public IActionResult Post([FromBody] Admin admin)
    {
        var isAdminAlreadyCreated = _collection.Find(a => a.Username == admin.Username).FirstOrDefault();

        if (isAdminAlreadyCreated != null)
        {
            return BadRequest("Invalid username");

        }

        if (admin == null || string.IsNullOrWhiteSpace(admin.Username) || string.IsNullOrWhiteSpace(admin.Password))
        {
            return BadRequest("Invalid username or password");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.Password);

        var adminToInsert = new Admin(admin.Username, hashedPassword);

        _collection.InsertOne(adminToInsert);

        return Ok("Admin stored successfully");
    }
}
