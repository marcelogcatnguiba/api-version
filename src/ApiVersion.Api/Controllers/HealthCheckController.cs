using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

public partial class HealthCheckController : BaseController
{
    
}

public partial class HealthCheckController : BaseController
{
    [HttpGet("healthcheck")]
    [MapToApiVersion(2)]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public IActionResult GetHealthV2([FromQuery] string body) => Ok(new { Response = body });
}