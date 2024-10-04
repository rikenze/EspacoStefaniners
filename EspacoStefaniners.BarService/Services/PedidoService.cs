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

        public async Task<IEnumerable<GetPedidoDTO>> GetAllPedidosAsync()
        {
            var pedidos = await _pedidoRepository.GetAllPedidosAsync();
            var pedidosDTO = new List<GetPedidoDTO>();
            foreach (var pedido in pedidos)
            {
                var pedidoDTO =_mapper.Map<GetPedidoDTO>(pedido);
                pedidoDTO.ValorTotal = pedidoDTO.ItensPedido.Sum(x => x.Quantidade * x.ValorUnitario);
                pedidosDTO.Add(pedidoDTO);
            }
            return pedidosDTO;
        }

        public async Task<GetPedidoDTO?> GetPedidoByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(id);
            return _mapper.Map<GetPedidoDTO>(pedido);
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
            foreach (var item in novoPedido.ItensPedido)
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

        public async Task<GetPedidoDTO> UpdatePedidoAsync(int id, Pedido pedido)
        {
            var pedidoDb = await _pedidoRepository.UpdatePedidoAsync(id, pedido);
            return _mapper.Map<GetPedidoDTO>(pedidoDb);
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            return await _pedidoRepository.DeletePedidoAsync(id);
        }
    }
}