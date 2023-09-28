using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Authorize]
[Route("table/{number}")]
[ApiController]

public class TableUpdate : ControllerBase
{
    private readonly TableServices _tableServices;


    public TableUpdate(TableServices tableServices)
    {
        _tableServices = tableServices;
    }

    [HttpPut]

    public async Task<IActionResult> Put([FromRoute] string number, [FromBody] List<OrderItem> ConsumedProducts)
    {
        var existingTable = await _tableServices.GetTableByNumber(number);

        if (existingTable == null)
        {
            return NotFound("Table not found");
        }

        existingTable.Number = number;
        existingTable.ConsumedProducts = ConsumedProducts;

        await _tableServices.UpdateTable(existingTable);

        return Ok("Table stored successfully");
    }
}
