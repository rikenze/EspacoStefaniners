
using EspacoStefaniners.BarService.Models;

namespace EspacoStefaniners.BarService.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IList<Produto>> GetAllProdutosAsync();
        Task<Produto> GetProdutoPorIdAsync(int id);
        Task<Produto> AddProduto(Produto produto);
        Task<Produto?> AtualizarProdutoAsync(int id, Produto produto);
        Task<Produto?> DeletarProdutoAsync(int id);
    }
}