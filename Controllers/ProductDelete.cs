using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Restaurant.Controllers;

[Route("product/{id}")]
[ApiController]

public class ProductDelete : ControllerBase
{
    private readonly IMongoCollection<Product> _collection;

    public ProductDelete(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    [HttpDelete]

    public IActionResult Delete([FromRoute] string id)
    {

        var Product = _collection.Find(p => p.Id == id).First();
        if (Product == null)
        {
            return NotFound("Product not found");
        }

        _collection.DeleteOne(p => p.Id == id);


        return Ok("Product Deleted");
    }

}
