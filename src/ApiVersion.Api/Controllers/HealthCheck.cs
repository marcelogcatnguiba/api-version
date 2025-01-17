using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiController]
[Route("/")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IResult CheckHealth()
    {
        return TypedResults.Ok(new { message = "OK" });
    }    
}
