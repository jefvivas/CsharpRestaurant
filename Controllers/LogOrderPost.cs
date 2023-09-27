using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Route("orderlog")]
[ApiController]
public class LogOrderPost : ControllerBase
{
    private readonly OrderLogServices _logServices;


    public LogOrderPost(OrderLogServices logServices)
    {
        _logServices = logServices;
    }

    [HttpPost]

    public async Task<IActionResult> Post([FromBody] OrderLog log)
    {

        if (log == null || log.Total == 0)
        {
            return BadRequest("Not able to store log");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }


        await _logServices.CreateLog(log);

        return Ok("Log stored successfully");
    }
}
