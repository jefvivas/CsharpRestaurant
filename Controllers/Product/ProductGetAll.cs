using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

[Authorize(Policy = "AdminOrUserPolicy")]
[Authorize]
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
    public async Task<IActionResult> Get([FromQuery] bool? onlyAvailable)
    {
        var Products = !onlyAvailable.HasValue || (onlyAvailable.HasValue && onlyAvailable.Value)
            ? await _productServices.GetAllAvailableProducts()
            : await _productServices.GetAllProducts();

        if (!Products.Any())
        {
            return NotFound("No products!");
        }

        var ProductResponse = Products.Select(p => new ProductGetResponse
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Category = p.Category,
            Description = p.Description,
            isAvailable = p.IsAvailable
        });

        return Ok(ProductResponse);
    }
}
