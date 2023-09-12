using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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

        var Product = _collection.Find(p => p.Id == id).First();
        if (Product == null)
        {
            return NotFound("Product not found!");
        }
        var ProductResponse = new ProductGetResponse { Id = Product.Id, Name = Product.Name, Price = Product.Price };


        return Ok(ProductResponse);
    }


}
