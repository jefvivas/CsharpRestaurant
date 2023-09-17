using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Authorize(AuthenticationSchemes = "adminJWT")]
[Route("admin")]
[ApiController]
public class AdminPost : ControllerBase
{
    private readonly AdminServices _adminServices;
    private readonly HashServices _hashServices;

    public AdminPost(AdminServices adminServices, HashServices hashServices)
    {
        _adminServices = adminServices;
        _hashServices = hashServices;
    }

    [HttpPost]

    public async Task<IActionResult> Post([FromBody] Admin admin)
    {
        var isAdminAlreadyCreated = await _adminServices.GetAdminByUsername(admin.Username);

        if (isAdminAlreadyCreated != null)
        {
            return BadRequest("Invalid username");

        }

        if (admin == null || string.IsNullOrWhiteSpace(admin.Username) || string.IsNullOrWhiteSpace(admin.Password))
        {
            return BadRequest("Invalid username or password");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }

        var hashedPassword = _hashServices.CreateHashedPassword(admin.Password);

        var adminToInsert = new Admin(admin.Username, hashedPassword);

        await _adminServices.CreateAdmin(adminToInsert);

        return Ok("Admin stored successfully");
    }
}
