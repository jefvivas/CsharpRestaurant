using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("product")]
[ApiController]
public class ProductGetAll : ControllerBase
{

    private readonly IMongoCollection<Product> _collection;

    public ProductGetAll(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    [HttpGet]

    public IActionResult Get()
    {

        var Products = _collection.Find(p => p.isAvailable).ToList();
        if (Products.Count == 0)
        {
            return NotFound("No products!");
        }
        var ProductResponse = Products.Select(p => new ProductGetResponse { Id = p.Id, Name = p.Name, Price = p.Price });


        return Ok(ProductResponse);
    }


}
