using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Restaurant.Models;
using System.IdentityModel.Tokens.Jwt;


namespace Restaurant.Controllers;
[Authorize]
[ApiController]
[Route("consume")]

public class AddProductsToTable : ControllerBase
{
    private readonly IMongoCollection<Table> _collection;

    public AddProductsToTable(IMongoCollection<Table> collection)
    {
        _collection = collection;
    }

    [HttpGet]

    public IActionResult Get()
    {
        var authHeader = HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authHeader))
        {
            return BadRequest("Token is needed.");
        }

        var tokenParts = authHeader.Split(' ');

        if (tokenParts.Length != 2 || tokenParts[0] != "Bearer")
        {
            return BadRequest("Invalid Token.");
        }

        var token = tokenParts[1];
        var handler = new JwtSecurityTokenHandler();
        var tokens = handler.ReadJwtToken(token);

        var uniqueNameClaim = tokens.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

        if (string.IsNullOrWhiteSpace(uniqueNameClaim))
        {
            return BadRequest("Unique name not found.");
        }

        var table = _collection.Find(t => t.Number == uniqueNameClaim).FirstOrDefault();

        if (table == null)
        {
            return NotFound("Table not found.");
        }

        if (table.ConsumedProducts.ContainsKey("6501f49e735c2c453b53ed0a"))

        {
            table.ConsumedProducts["6501f49e735c2c453b53ed0a"] += 1;
        }
        else
        {
            table.ConsumedProducts.Add("6501f49e735c2c453b53ed0a", 1);
        }

        var filter = Builders<Table>.Filter.Eq(t => t.Number, uniqueNameClaim);
        var update = Builders<Table>.Update
            .Set(t => t.ConsumedProducts, table.ConsumedProducts);

        var result = _collection.ReplaceOne(filter, table);

        if (result.ModifiedCount != 1)
        {
            return NotFound("Table not found.");

        }

        return Ok("added product to table");
    }


}
