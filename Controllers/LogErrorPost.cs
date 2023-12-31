﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.Services;

namespace Restaurant.Controllers;

[Route("errorlog")]
[ApiController]
public class LogErrorPost : ControllerBase
{
    private readonly ErrorLogServices _logServices;


    public LogErrorPost(ErrorLogServices logServices)
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
