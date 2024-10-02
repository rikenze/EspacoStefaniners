
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Data.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IList<Produto>> GetAllProdutosAsync();
        Task<Produto> GetProdutoPorIdAsync(int id);
        Task<Produto> AddProduto(Produto produto);
        Task<Produto?> AtualizarProdutoAsync(int id, Produto produto);
        Task<Produto?> DeletarProdutoAsync(int id);
    }
}