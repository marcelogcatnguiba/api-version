using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/")]
public abstract class BaseHealthCheckController : ControllerBase
{
    [HttpGet("healthcheck")]
    [MapToApiVersion(1)]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetHealth() => Ok(new { message = "OK" });
}
