using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Data.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<Pedido> AddPedidoAsync(Pedido pedido);
        Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido);
        Task<bool> DeletePedidoAsync(int id);
    }
}