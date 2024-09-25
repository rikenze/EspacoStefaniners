using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Services
{
    public class BebidasService : IBebidasService
    {
        private readonly IBarContext _context;

        public BebidasService(IBarContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetBebidaPorIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<IList<Produto>> GetAllBebidasAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> AddBebida(Produto bebida)
        {
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