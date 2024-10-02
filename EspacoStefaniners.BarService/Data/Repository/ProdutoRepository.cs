
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using Microsoft.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IBarContext _context;

        public ProdutoRepository(IBarContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetProdutoPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<IList<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> AddProduto(Produto bebida)
        {
            var produto = await _context.Produtos.FindAsync(bebida.Id);

            if (produto != null)
            {
                return null;
            }

            await _context.Produtos.AddAsync(bebida);
            await _context.SaveChangesAsync();
            return bebida;
        }

        public async Task<Produto?> AtualizarProdutoAsync(int id, Produto bebida)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return null;
            }

            produto.NomeProduto = bebida.NomeProduto;
            produto.Valor = bebida.Valor;

            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto?> DeletarProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return null;
            }
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
    }
}