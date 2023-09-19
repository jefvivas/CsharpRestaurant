using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

[Route("product/{id}")]
[ApiController]
public class ProductGetById : ControllerBase
{

    private readonly ProductServices _productServices;

    public ProductGetById(ProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpGet]

    public async Task<IActionResult> Get([FromRoute] string id)
    {

        var product = await _productServices.GetProductById(id);

        if (product == null)
        {
            return NotFound("Product not found!");
        }
        var ProductResponse = new ProductGetResponse { Id = product.Id, Name = product.Name, Price = product.Price, Category = product.Category, Description = product.Description };


        return Ok(ProductResponse);
    }


}
