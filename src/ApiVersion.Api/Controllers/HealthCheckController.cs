using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiVersion(2)]
public class HealthCheckController : BaseController
{
    [HttpGet("healthcheck")]
    [MapToApiVersion(2)]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult GetApiStatus([FromQuery] string body) => Ok(new { Response = body });
}
