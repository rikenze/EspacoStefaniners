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
    }
}