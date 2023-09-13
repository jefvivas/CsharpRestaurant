using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Authorize]
[Route("product/{id}")]
[ApiController]
public class ProductGetById : ControllerBase
{

    private readonly IMongoCollection<Product> _collection;

    public ProductGetById(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    [HttpGet]

    public IActionResult Get([FromRoute] string id)
    {

        var product = _collection.Find(p => p.Id == id).First();
        if (product == null)
        {
            return NotFound("Product not found!");
        }
        var ProductResponse = new ProductGetResponse { Id = product.Id, Name = product.Name, Price = product.Price };


        return Ok(ProductResponse);
    }


}
