using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/")]
public abstract class BaseController : ControllerBase
{
    [HttpGet("healthcheck")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult GetHealth() => Ok(new { message = "OK" });
}
