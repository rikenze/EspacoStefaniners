using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtosService;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IProdutoService produtosService, ILogger<ProdutosController> logger)
    {
        _produtosService = produtosService;
        _logger = logger;
    }

    /// <summary>
    /// Obtém um produto pelo ID.
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Retorna a produto correspondente ao ID</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetProdutoPorId(int id)
    {
        var bebida = await _produtosService.GetProdutoPorIdAsync(id);

        if (bebida == null)
        {
            return NotFound(); // Retorna 404 se não encontrar produtos
        }

        return Ok(bebida);
    }

    /// <summary>
    /// Obtém todos os produtos.
    /// </summary>
    /// <returns>Retorna a lista de todos os produtos</returns>
    [HttpGet]
    public async Task<ActionResult<IList<Produto>>> GetAll()
    {
        var produtos = await _produtosService.GetAllProdutosAsync();

        if (produtos == null)
        {
            return NotFound(); // Retorna 404 se não encontrar produtos
        }

        return Ok(produtos);
    }

    /// <summary>
    /// Adiciona um novo produto.
    /// </summary>
    /// <param name="produto">Dados do novo produto</param>
    /// <returns>Retorna o produto criado</returns>
    [HttpPost]
    public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
    {
        var result = await _produtosService.AddProduto(produto);
        if (result == null)
            return BadRequest();
        return CreatedAtAction(nameof(GetProdutoPorId), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Atualiza um produto existente.
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <param name="produto">Dados atualizados do produto</param>
    /// <returns>Retorna o produto atualizado</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Produto>> Put(int id, [FromBody] Produto produto)
    {
        var result = await _produtosService.AtualizarProdutoAsync(id, produto);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Deleta um produto pelo ID.
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Retorna o produto deletado</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Produto>> Delete(int id)
    {
        var result = await _produtosService.DeletarProdutoAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
