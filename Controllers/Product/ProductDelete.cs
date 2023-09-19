using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("product/{id}")]
[ApiController]

public class ProductDelete : ControllerBase
{
    private readonly ProductServices _productServices;

    public ProductDelete(ProductServices productServices)
    {
        _productServices = productServices;
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {

        var product = await _productServices.GetProductById(id);

        if (product == null)
        {
            return NotFound("Product not found");
        }

        await _productServices.DeleteProduct(id);


        return Ok("Product Deleted");
    }

}
