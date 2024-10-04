using AutoMapper;
using EspacoStefaniners.BarService.Data.DTO;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EspacoStefaniners.BarService.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar os pedidos.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        /// <summary>
        /// Construtor da classe PedidosController.
        /// </summary>
        /// <param name="pedidoService">Serviço de pedidos injetado.</param>
        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Obtém todos os pedidos.
        /// </summary>
        /// <returns>Uma lista de pedidos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetPedidoDTO>>> GetAll()
        {
            var pedidos = await _pedidoService.GetAllPedidosAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Obtém um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido.</param>
        /// <returns>O pedido correspondente ao ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetById(int id)
        {
            var pedido = await _pedidoService.GetPedidoByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <param name="criarPedidoDTO"></param>
        /// <param name="pedido">Dados do novo pedido.</param>
        /// <returns>O pedido criado.</returns>
        [HttpPost]
        public async Task<ActionResult<Pedido>> Create([FromBody] CriarPedidoDTO criarPedidoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var novoPedido = await _pedidoService.AddPedidoAsync(criarPedidoDTO);

                return CreatedAtAction(nameof(GetById), new { id = novoPedido.Id }, novoPedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        /// <summary>
        /// Atualiza um pedido existente.
        /// </summary>
        /// <param name="id">ID do pedido a ser atualizado.</param>
        /// <param name="pedido">Dados atualizados do pedido.</param>
        /// <returns>O pedido atualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Pedido>> Update(int id, [FromBody] Pedido pedido)
        {
            var updatedPedido = await _pedidoService.UpdatePedidoAsync(id, pedido);
            if (updatedPedido == null) return NotFound();
            return Ok(updatedPedido);
        }

        /// <summary>
        /// Exclui um pedido pelo ID.
        /// </summary>
        /// <param name="id">ID do pedido a ser excluído.</param>
        /// <returns>Resultado da exclusão.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _pedidoService.DeletePedidoAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}