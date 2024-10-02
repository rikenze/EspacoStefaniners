using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using Microsoft.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Data
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IBarContext _context;

        public PedidoRepository(IBarContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            return await _context.Pedidos.Include(p => p.Itens).ToListAsync();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> AddPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido)
        {
            var existingPedido = await _context.Pedidos.FindAsync(id);
            if (existingPedido == null) return null;

            existingPedido.NomeCliente = pedido.NomeCliente;
            existingPedido.EmailCliente = pedido.EmailCliente;
            existingPedido.DataCriacao = pedido.DataCriacao;
            existingPedido.Pago = pedido.Pago;
            existingPedido.Itens = pedido.Itens;

            await _context.SaveChangesAsync();
            return existingPedido;
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}