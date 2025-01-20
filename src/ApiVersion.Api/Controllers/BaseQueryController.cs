using ApiVersion.Api.UserAttributes;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiController]
[UserApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseQueryController<T> : ControllerBase where T : class
{
    [HttpGet]
    [MapToApiVersion(1)]
    public ActionResult<T> GetEntity()
    {
        return Ok(Activator.CreateInstance(typeof(T)));
    }
}
