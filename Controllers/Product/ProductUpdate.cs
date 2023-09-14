using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("product/{id}")]
[ApiController]
public class ProductUpdate : ControllerBase
{

    private readonly ProductServices _productServices;

    public ProductUpdate(ProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpPut]

    public IActionResult Put([FromRoute] string id, [FromBody] Product updatedProduct)
    {

        var existingProduct = _productServices.GetProductByName(id);

        if (existingProduct == null)
        {
            return NotFound("Product not found");
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.isAvailable = updatedProduct.isAvailable;
        existingProduct.Price = updatedProduct.Price;

        _productServices.UpdateProduct(existingProduct);

        return Ok(existingProduct);
    }
}
