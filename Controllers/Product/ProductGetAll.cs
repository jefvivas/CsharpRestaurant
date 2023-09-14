using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Restaurant.Services;

[Route("product")]
[ApiController]
public class ProductGetAll : ControllerBase
{

    private readonly ProductServices _productServices;

    public ProductGetAll(ProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]

    public IActionResult Get()
    {

        var Products = _productServices.GetAllProducts();

        if (!Products.Any())
        {
            return NotFound("No products!");
        }
        var ProductResponse = Products.Select(p => new ProductGetResponse { Id = p.Id, Name = p.Name, Price = p.Price });


        return Ok(ProductResponse);
    }


}
