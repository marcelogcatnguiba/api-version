using ApiVersion.Api.Model;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersion.Api.Controllers;

[ApiController]
[ApiVersion(1)]
[ApiVersion(2)]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonagemController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(1)]
    public ActionResult<List<Personagem>> GetAll_Slug2()
    {
        return Ok(ListPersonagem().Take(4));
    }

    [HttpGet]
    [MapToApiVersion(2)]
    public ActionResult<List<Personagem>> GetAll_Slug4()
    {
        return Ok(ListPersonagem().Skip(4));
    }

    private static List<Personagem> ListPersonagem() => 
    [ 
        new(1, "Marco Rossi", "Major"),
        new(2, "Tarma Roving", "Captain"),
        new(3, "Eri Kasamoto", "Sargent"),
        new(4, "Fio Germi", "Major Sargent"),
        new(5, "Travor Spacey", "Sargent"),
        new(6, "Nadia Cassel", "SPARROWS")
    ];
}
