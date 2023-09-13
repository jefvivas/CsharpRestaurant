using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("product/{id}")]
[ApiController]
public class ProductUpdate : ControllerBase
{

    private readonly IMongoCollection<Product> _collection;

    public ProductUpdate(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    [HttpPut]

    public IActionResult Put([FromRoute] string id, [FromBody] Product updatedProduct)
    {

        var existingProduct = _collection.Find(p => p.Id == id).First();

        if (existingProduct == null)
        {
            return NotFound("Product not found");
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.isAvailable = updatedProduct.isAvailable;
        existingProduct.Price = updatedProduct.Price;

        _collection.ReplaceOne(p => p.Id == id, existingProduct);


        return Ok(existingProduct);
    }
}
