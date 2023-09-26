using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Route("log")]
[ApiController]
public class LogErrorPost : ControllerBase
{
    private readonly LogServices _logServices;


    public LogErrorPost(LogServices logServices)
    {
        _logServices = logServices;
    }

    [HttpPost]

    public async Task<IActionResult> Post([FromBody] ErrorLog log)
    {

        if (log == null || string.IsNullOrWhiteSpace(log.Message))
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
