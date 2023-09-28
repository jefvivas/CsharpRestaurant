using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Authorize]
[ApiController]
[Route("consume")]
public class TableGetAllItems : ControllerBase
{
    private readonly TableServices _tableServices;
    private readonly JwtServices _jwtService;


    public TableGetAllItems(TableServices tableServices, JwtServices jwtService)
    {
        _tableServices = tableServices;
        _jwtService = jwtService;

    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var uniqueNameClaim = _jwtService.GetUniqueNameClaim(HttpContext);

        if (string.IsNullOrWhiteSpace(uniqueNameClaim))
        {
            return BadRequest("Invalid Token.");
        }

        var tableData = await _tableServices.GetTableByNumber(uniqueNameClaim);

        var response = new TableAllItemsModel
        {
            Items = tableData.ConsumedProducts.Select(p => new ProductItem
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList()
        };

        return Ok(response);
    }
}
