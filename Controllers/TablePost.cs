using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("table")]
[ApiController]
public class TablePost : ControllerBase
{
    private readonly TableServices _tableServices;

    public TablePost(TableServices tableServices)
    {
        _tableServices = tableServices;
    }

    [HttpPost]

    public IActionResult Post([FromBody] Table table)
    {
        var isTableAlreadyCreated = _tableServices.GetTableByNumber(table.Number);

        if (isTableAlreadyCreated != null)
        {
            return BadRequest("Invalid Number");

        }

        if (table == null || string.IsNullOrWhiteSpace(table.Number) || string.IsNullOrWhiteSpace(table.Password))
        {
            return BadRequest("Invalid number or password");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(table.Password);

        var tableToInsert = new Table(table.Number, hashedPassword);

        _tableServices.CreateTable(tableToInsert);

        return Ok("Table stored successfully");
    }
}
