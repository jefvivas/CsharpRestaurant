using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Restaurant.Models;
namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("table")]
[ApiController]
public class TablePost : ControllerBase
{
    private readonly IMongoCollection<Table> _collection;

    public TablePost(IMongoCollection<Table> collection)
    {
        _collection = collection;
    }

    [HttpPost]

    public IActionResult Post([FromBody] Table table)
    {
        var isTableAlreadyCreated = _collection.Find(t => t.Number == table.Number).FirstOrDefault();

        if (isTableAlreadyCreated != null)
        {
            return BadRequest("Invalid Number");

        }

        if (table == null || string.IsNullOrWhiteSpace(table.Number) || string.IsNullOrWhiteSpace(table.Password))
        {
            return BadRequest("Invalid number or password");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(table.Password);

        var tableToInsert = new Table(table.Number, hashedPassword);

        _collection.InsertOne(tableToInsert);

        return Ok("Table stored successfully");
    }
}
