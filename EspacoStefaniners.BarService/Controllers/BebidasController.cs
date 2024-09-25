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
    public StatusCodeResult Get()
    {
        return StatusCode(200);
    }
}