using EspacoStefaniners.BarService.Data.DTO;
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync();
        Task<Pedido> GetPedidoByIdAsync(int id);
        Task<GetPedidoDTO> AddPedidoAsync(CriarPedidoDTO criarPedidoDTO);
        Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido);
        Task<bool> DeletePedidoAsync(int id);
    }
}