using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Route("/login")]
[ApiController]
public class TableLogin : ControllerBase
{
    private readonly TableServices _tableServices;
    private readonly JwtServices _jwtService;
    private readonly HashServices _hashService;



    public TableLogin(TableServices tableServices, JwtServices jwtService, HashServices hashService)
    {
        _tableServices = tableServices;
        _jwtService = jwtService;
        _hashService = hashService;
    }

    [HttpPost]

    public IActionResult Post([FromBody] TableLoginInput credentials)
    {
        var tableFound = _tableServices.GetTableByNumber(credentials.Number);

        if (tableFound == null)
        {
            return Unauthorized("Number/password dont match");

        }
        if (_hashService.VerifyPassword(credentials.Password, tableFound.Password))
        {
            var token = _jwtService.GenerateJwtToken(credentials.Number, "08D856F45E32C98D0AA162BBD99E99D5");

            return Ok(new { Token = token });
        }

        return Unauthorized("Number/password dont match");
    }


}
