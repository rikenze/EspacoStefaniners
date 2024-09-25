using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EspacoStefaniners.BarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ItensPedidoController : ControllerBase
    {
        private readonly IItensPedidoService _itensPedidoService;

        public ItensPedidoController(IItensPedidoService itensPedidoService)
        {
            _itensPedidoService = itensPedidoService;
        }

        /// <summary>
        /// Obtém todos os itens de pedido.
        /// </summary>
        /// <returns>Retorna a lista de todos os itens de pedido</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItensPedido>>> GetAll()
        {
            var itensPedidos = await _itensPedidoService.GetAllItensPedidoAsync();
            return Ok(itensPedidos);
        }

        /// <summary>
        /// Obtém um item de pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do item de pedido</param>
        /// <returns>Retorna o item de pedido correspondente ao ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItensPedido>> GetById(int id)
        {
            var itensPedido = await _itensPedidoService.GetItensPedidoByIdAsync(id);
            if (itensPedido == null) return NotFound();
            return Ok(itensPedido);
        }

        /// <summary>
        /// Adiciona um novo item de pedido.
        /// </summary>
        /// <param name="itensPedido">Dados do item de pedido</param>
        /// <returns>Retorna o item de pedido criado</returns>
        [HttpPost]
        public async Task<ActionResult<ItensPedido>> Create([FromBody] ItensPedido itensPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItensPedido = await _itensPedidoService.AddItensPedidoAsync(itensPedido);
            return CreatedAtAction(nameof(GetById), new { id = newItensPedido.Id }, newItensPedido);
        }

        /// <summary>
        /// Atualiza um item de pedido existente.
        /// </summary>
        /// <param name="id">ID do item de pedido</param>
        /// <param name="itensPedido">Dados atualizados do item de pedido</param>
        /// <returns>Retorna o item de pedido atualizado</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ItensPedido>> Update(int id, [FromBody] ItensPedido itensPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedItensPedido = await _itensPedidoService.UpdateItensPedidoAsync(id, itensPedido);
            if (updatedItensPedido == null) return NotFound();
            return Ok(updatedItensPedido);
        }

        /// <summary>
        /// Deleta um item de pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do item de pedido</param>
        /// <returns>Retorna o item de pedido deletado</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _itensPedidoService.DeleteItensPedidoAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}