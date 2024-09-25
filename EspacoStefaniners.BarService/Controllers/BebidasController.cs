using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace EspacoStefaniners.BarService.Controllers;

[ApiController]
[Route("[controller]")]
public class BebidasController : ControllerBase
{
    private readonly ILogger<BebidasController> _logger;

    public BebidasController(ILogger<BebidasController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetBebidas")]
    public ActionResult Get()
    {
        return StatusCode(200);
    }

    [HttpGet(Name = "AdicionarBebida")]
    public ActionResult Post()
    {
        return StatusCode(200);
    }

    [HttpGet(Name = "EditarBebida")]
    public ActionResult Put()
    {
        return StatusCode(200);
    }

    [HttpGet(Name = "DeletarBebida")]
    public ActionResult Delete()
    {
        return StatusCode(200);
    }
}