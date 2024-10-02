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
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor da classe PedidosController.
        /// </summary>
        /// <param name="pedidoService">Serviço de pedidos injetado.</param>
        public PedidosController(IMapper mapper, IPedidoService pedidoService)
        {
            _mapper = mapper;
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Obtém todos os pedidos.
        /// </summary>
        /// <returns>Uma lista de pedidos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
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
                Pedido pedido = _mapper.Map<Pedido>(criarPedidoDTO);
                var novoPedido = await _pedidoService.AddPedidoAsync(pedido);

                if (novoPedido == null)
                {
                    return BadRequest("Erro ao criar o pedido.");
                }
                
                var itensPedido = new List<GetItemPedidoDTO>();
                foreach(var item in novoPedido.Itens)
                    itensPedido.Add(_mapper.Map<GetItemPedidoDTO>(item));
         
                var getPedidoCriado = new GetPedidoDTO
                {
                    Id = novoPedido.Id,
                    NomeCliente = novoPedido.NomeCliente,
                    EmailCliente = novoPedido.EmailCliente,
                    Pago = novoPedido.Pago,
                    ValorTotal = itensPedido.Sum(x => x.Quantidade * x.Produto.Valor),
                    ItensPedido = itensPedido
                };

                return CreatedAtAction(nameof(GetById), new { id = novoPedido.Id }, getPedidoCriado);
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