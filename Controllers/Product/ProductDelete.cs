using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
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

        var Product = _collection.Find(p => p.Id == id).FirstOrDefault();

        if (Product == null)
        {
            return NotFound("Product not found");
        }

        _collection.DeleteOne(p => p.Id == id);


        return Ok("Product Deleted");
    }

}
