using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Authorize(AuthenticationSchemes = "adminJWT")]
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
        var isProductAlreadyCreated = _collection.Find(p => p.Name == product.Name).FirstOrDefault();

        if (isProductAlreadyCreated != null)
        {
            return BadRequest("Product Already Exists");

        }

        if (!product.IsValid())
        {
            return BadRequest("Invalid Product");
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
