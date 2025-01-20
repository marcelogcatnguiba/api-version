using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/")]
public abstract class BaseController : ControllerBase
{
    [HttpGet("healthcheck")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [MapToApiVersion(1)]
    public IActionResult GetHealth() => Ok(new { message = "OK" });
}