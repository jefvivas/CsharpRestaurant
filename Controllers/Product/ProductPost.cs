using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("product")]
[ApiController]
public class ProductPost : ControllerBase
{
    private readonly ProductServices _productServices;

    public ProductPost(ProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        var isProductAlreadyCreated = _productServices.GetProductByName(product.Name);

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

        _productServices.CreateProduct(productToInsert);

        return Ok("Product stored successfully");
    }


}
