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

    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] Product updatedProduct)
    {

        var existingProduct = await _productServices.GetProductByName(id);

        if (existingProduct == null)
        {
            return NotFound("Product not found");
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.IsAvailable = updatedProduct.IsAvailable;
        existingProduct.Description = updatedProduct.Description;
        existingProduct.Price = updatedProduct.Price;

        await _productServices.UpdateProduct(existingProduct);

        return Ok(existingProduct);
    }
}
