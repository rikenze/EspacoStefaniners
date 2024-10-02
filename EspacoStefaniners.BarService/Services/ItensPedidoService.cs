using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;

namespace EspacoStefaniners.BarService.Services
{
    public class ItensPedidoService : IItensPedidoService
    {
        private readonly IItensPedidoRepository _itensPedidoRepository;

        public ItensPedidoService(IItensPedidoRepository itensPedidoRepository)
        {
            _itensPedidoRepository = itensPedidoRepository;
        }

        public async Task<List<ItemPedido>> GetAllItensPedidoAsync()
        {
            return await _itensPedidoRepository.GetAllItensPedidoAsync();
        }

        public async Task<ItemPedido?> GetItensPedidoByIdAsync(int id)
        {
            return await _itensPedidoRepository.GetItensPedidoByIdAsync(id);
        }

        public async Task<ItemPedido?> GetItensPedidoByIdPedidoAsync(int id)
        {
            return await _itensPedidoRepository.GetItensPedidoByIdPedidoAsync(id);
        }

        public async Task<List<ItemPedido>> AddItensPedidoAsync(List<ItemPedido> itensPedido)
        {
            return await _itensPedidoRepository.AddItensPedidoAsync(itensPedido);
        }

        public async Task<ItemPedido> UpdateItensPedidoAsync(int id, ItemPedido itemPedido)
        {
           return await _itensPedidoRepository.UpdateItensPedidoAsync(id, itemPedido);
        }

        public async Task<bool> DeleteItensPedidoAsync(int id)
        {
            return await _itensPedidoRepository.DeleteItensPedidoAsync(id);
        }
    }
}