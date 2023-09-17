using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Services;


namespace Restaurant.Controllers;
[Authorize]
[ApiController]
[Route("consume")]

public class AddProductsToTable : ControllerBase
{
    private readonly TableServices _tableServices;
    private readonly JwtServices _jwtService;


    public AddProductsToTable(TableServices tableServices, JwtServices jwtService)
    {
        _tableServices = tableServices;
        _jwtService = jwtService;

    }

    [HttpPost]

    public async Task<IActionResult> Post([FromBody] ConsumePostBody consumeBody)
    {
        if (!consumeBody.Items.Any())
        {
            return BadRequest("You should send at least 1 product");
        }
        var uniqueNameClaim = _jwtService.GetUniqueNameClaim(HttpContext);

        if (string.IsNullOrWhiteSpace(uniqueNameClaim))
        {
            return BadRequest("Invalid Token.");
        }

        var table = await _tableServices.GetTableByNumber(uniqueNameClaim);

        if (table == null)
        {
            return NotFound("Table not found.");
        }

        await _tableServices.InsertProductsIntoTable(table, consumeBody);

        return Ok("added product to table");
    }

}
