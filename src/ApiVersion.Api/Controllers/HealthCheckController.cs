using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiVersion(2)]
public class HealthCheckController : BaseHealthCheckController
{
    [HttpGet("healthcheck")]
    [MapToApiVersion(2)]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult GetApiStatus() => Ok(new { IsAccessible = "OK" });
}
