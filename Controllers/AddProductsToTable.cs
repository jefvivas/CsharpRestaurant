using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Restaurant.Models;
using Restaurant.Services;


namespace Restaurant.Controllers;
[Authorize]
[ApiController]
[Route("consume")]

public class AddProductsToTable : ControllerBase
{
    private readonly IMongoCollection<Table> _collection;
    private readonly JwtService _jwtService;


    public AddProductsToTable(IMongoCollection<Table> collection, JwtService jwtService)
    {
        _collection = collection;
        _jwtService = jwtService;

    }

    [HttpPost]

    public IActionResult Post([FromBody] ConsumePostBody consumeBody)
    {
        var uniqueNameClaim = _jwtService.GetUniqueNameClaim(HttpContext);

        if (string.IsNullOrWhiteSpace(uniqueNameClaim))
        {
            return BadRequest("Invalid Token.");
        }

        var table = _collection.Find(t => t.Number == uniqueNameClaim).FirstOrDefault();

        if (table == null)
        {
            return NotFound("Table not found.");
        }

        foreach (var item in consumeBody.Items)
        {
            if (!table.ConsumedProducts.ContainsKey(item.ProductId))

            {
                table.ConsumedProducts.Add(item.ProductId, item.Quantity);

            }
            else
            {
                table.ConsumedProducts[item.ProductId] += item.Quantity;
            }


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
