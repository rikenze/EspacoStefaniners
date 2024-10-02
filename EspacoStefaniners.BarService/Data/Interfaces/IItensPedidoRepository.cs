using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Data.Interfaces
{
    public interface IItensPedidoRepository
    {
        Task<List<ItemPedido>> GetAllItensPedidoAsync();
        Task<ItemPedido> GetItensPedidoByIdAsync(int id);
        Task<List<ItemPedido>> AddItensPedidoAsync(List<ItemPedido> itensPedido);
        Task<ItemPedido> UpdateItensPedidoAsync(int id, ItemPedido itemPedido);
        Task<bool> DeleteItensPedidoAsync(int id);
        Task<ItemPedido?> GetItensPedidoByIdPedidoAsync(int id);
    }
}