using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;

namespace EspacoStefaniners.BarService.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            return await _pedidoRepository.GetAllPedidosAsync();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _pedidoRepository.GetPedidoByIdAsync(id);
        }

        public async Task<Pedido> AddPedidoAsync(Pedido pedido)
        {
            return await _pedidoRepository.AddPedidoAsync(pedido);
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