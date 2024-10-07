using EspacoStefaniners.BarService.Data.DTO;

namespace EspacoStefaniners.BarService.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<GetPedidoDTO>> GetAllPedidosAsync();
        Task<GetPedidoDTO> GetPedidoByIdAsync(int id);
        Task<GetPedidoDTO> AddPedidoAsync(CriarPedidoDTO criarPedidoDTO);
        Task<GetPedidoDTO> UpdatePedidoAsync(int id, EditarPedidoDTO pedido);
        Task<bool> DeletePedidoAsync(int id);
    }
}