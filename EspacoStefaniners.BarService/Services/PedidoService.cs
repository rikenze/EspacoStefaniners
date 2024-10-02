using AutoMapper;
using EspacoStefaniners.BarService.Data.DTO;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;

namespace EspacoStefaniners.BarService.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoService(IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            return await _pedidoRepository.GetAllPedidosAsync();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetPedidoByIdAsync(id);
        }

        public async Task<GetPedidoDTO> AddPedidoAsync(CriarPedidoDTO criarPedidoDTO)
        {
            Pedido pedido = _mapper.Map<Pedido>(criarPedidoDTO);
            var novoPedido = await _pedidoRepository.AddPedidoAsync(pedido);

            if (novoPedido == null)
            {
                throw new Exception("Erro ao criar o pedido.");
            }

            var itensPedido = new List<GetItemPedidoDTO>();
            foreach (var item in novoPedido.Itens)
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

            return getPedidoCriado;
        }

        public async Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido)
        {
            return await _pedidoRepository.UpdatePedidoAsync(id, pedido);
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            return await _pedidoRepository.DeletePedidoAsync(id);
        }
    }
}