
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Services.Interfaces
{
    public interface IBebidasService
    {
         Task<IList<Produto>> GetAllBebidasAsync();
         Task<Produto> GetBebidaPorIdAsync(int id);
         Task<Produto> AddBebida(Produto bebida);
    }
}