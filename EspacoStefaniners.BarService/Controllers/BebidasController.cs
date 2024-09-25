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

    /// <summary>
    /// Obtém uma bebida pelo ID.
    /// </summary>
    /// <param name="id">ID da bebida</param>
    /// <returns>Retorna a bebida correspondente ao ID</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetBebidaPorI(int id)
    {
        var bebida = await _bebidasService.GetBebidaPorIdAsync(id);

        if (bebida == null)
        {
            return NotFound(); // Retorna 404 se não encontrar bebidas
        }

        return Ok(bebida);
    }

    /// <summary>
    /// Obtém todas as bebidas.
    /// </summary>
    /// <returns>Retorna a lista de todas as bebidas</returns>
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

    /// <summary>
    /// Adiciona uma nova bebida.
    /// </summary>
    /// <param name="produto">Dados da nova bebida</param>
    /// <returns>Retorna a bebida criada</returns>
    [HttpPost]
    public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
    {
        var result = await _bebidasService.AddBebida(produto);
        if (result == null)
            return BadRequest();
        return CreatedAtAction(nameof(GetBebidaPorI), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Atualiza uma bebida existente.
    /// </summary>
    /// <param name="id">ID da bebida</param>
    /// <param name="produto">Dados atualizados da bebida</param>
    /// <returns>Retorna a bebida atualizada</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Produto>> Put(int id, [FromBody] Produto produto)
    {
        var result = await _bebidasService.AtualizarProdutoAsync(id, produto);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Deleta uma bebida pelo ID.
    /// </summary>
    /// <param name="id">ID da bebida</param>
    /// <returns>Retorna a bebida deletada</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Produto>> Delete(int id)
    {
        var result = await _bebidasService.DeletarProdutoAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
