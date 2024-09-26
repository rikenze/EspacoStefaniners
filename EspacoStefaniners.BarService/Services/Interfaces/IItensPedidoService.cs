using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Services.Interfaces
{
    public interface IItensPedidoService
    {
        Task<IEnumerable<ItensPedido>> GetAllItensPedidoAsync();
        Task<ItensPedido> GetItensPedidoByIdAsync(int id);
        Task<ItensPedido> AddItensPedidoAsync(ItensPedido itensPedido);
        Task<ItensPedido> UpdateItensPedidoAsync(int id, ItensPedido itensPedido);
        Task<bool> DeleteItensPedidoAsync(int id);
        Task<ItensPedido?> GetItensPedidoByIdPedidoAsync(int id);
    }
}