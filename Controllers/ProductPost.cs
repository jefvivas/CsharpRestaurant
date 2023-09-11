using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;


[Route("product")]
[ApiController]
public class ProductPost : ControllerBase
{
    private readonly IMongoCollection<Product> _collection;

    public ProductPost(IMongoCollection<Product> collection)
    {
        _collection = collection;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        var isProductAlreadyCreated = _collection.Find(p => p.Name == product.Name).First();

        if (isProductAlreadyCreated != null)
        {
            return BadRequest("Invalid Name");

        }

        if (product.Price == 0)
        {
            return BadRequest("Invalid Price");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }

        var productToInsert = new Product(product.Name, product.Price, product.isAvailable);

        _collection.InsertOne(productToInsert);

        return Ok("Product stored successfully");
    }


}
