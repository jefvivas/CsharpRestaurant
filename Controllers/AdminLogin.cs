using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Route("/admin/login")]
[ApiController]
public class AdminLogin : ControllerBase
{

    private readonly AdminServices _adminServices;
    private readonly JwtServices _jwtService;
    private readonly HashServices _hashService;


    public AdminLogin(AdminServices adminServices, JwtServices jwtService, HashServices hashService)
    {
        _adminServices = adminServices;
        _jwtService = jwtService;
        _hashService = hashService;
    }

    [HttpPost]

    public async Task<IActionResult> Post([FromBody] AdminLoginInput credentials)
    {
        var adminFound = await _adminServices.GetAdminByUsername(credentials.Username);


        if (adminFound == null)
        {
            return Unauthorized("Username/password dont match");

        }
        if (_hashService.VerifyPassword(credentials.Password, adminFound.Password))
        {
            var token = _jwtService.GenerateJwtToken(credentials.Username, "3A9F041FD4B9E0C12D0B8F008F5E1B76D8DCA1CEBB36E5E586A81D5B936F276");

            return Ok(new { Token = token });
        }

        return Unauthorized("Username/passworde dont match");
    }

}
