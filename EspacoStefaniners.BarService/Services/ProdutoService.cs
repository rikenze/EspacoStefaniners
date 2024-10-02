using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;

namespace EspacoStefaniners.BarService.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto> GetProdutoPorIdAsync(int id)
        {
            return await _produtoRepository.GetProdutoPorIdAsync(id);
        }

        public async Task<IList<Produto>> GetAllProdutosAsync()
        {
            return await _produtoRepository.GetAllProdutosAsync();
        }

        public async Task<Produto?> AddProduto(Produto bebida)
        {
            return await _produtoRepository.AddProduto(bebida);
        }

        public async Task<Produto?> AtualizarProdutoAsync(int id, Produto produto)
        {
            return await _produtoRepository.AtualizarProdutoAsync(id, produto);
        }

        public async Task<Produto?> DeletarProdutoAsync(int id)
        {
            return await _produtoRepository.DeletarProdutoAsync(id);
        }
    }
}