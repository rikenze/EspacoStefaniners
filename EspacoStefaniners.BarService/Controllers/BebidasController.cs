using EspacoStefaniners.BarService.Services.Interfaces;
using EspacoStefaniners.BarService.Services;
using Microsoft.AspNetCore.Mvc;
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BebidasController : ControllerBase
{
    private readonly IBebidasService _bebidasService;
    private readonly ILogger<BebidasController> _logger;

    public BebidasController(IBebidasService bebidasService, ILogger<BebidasController> logger)
    {
        _bebidasService = bebidasService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBebidaPorI(int id)
    {
        var bebida = await _bebidasService.GetBebidaPorIdAsync(id);

        if (bebida == null)
        {
            return NotFound(); // Retorna 404 se não encontrar bebidas
        }

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IList<Produto>>> GetAll()
    {
        var bebidas = await _bebidasService.GetAllBebidasAsync();

        if (bebidas == null)
        {
            return NotFound(); // Retorna 404 se não encontrar bebidas
        }

        return Ok(bebidas);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
    {
        await _bebidasService.AddBebida(produto);
        return CreatedAtAction(nameof(GetBebidaPorI), new { id  = produto.Id}, produto);
    }

    [HttpPut]
    public ActionResult Put()
    {
        return StatusCode(200);
    }

    [HttpDelete]
    public ActionResult Delete()
    {
        return StatusCode(200);
    }
}