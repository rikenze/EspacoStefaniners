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
            return await _context.Pedidos.Include(p => p.ItensPedido).ThenInclude(p => p.Produto).ToListAsync();
        }

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.ItensPedido).ThenInclude(p => p.Produto).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> AddPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> UpdatePedidoAsync(Pedido pedido)
        {
            var existingPedido = await _context.Pedidos.FindAsync(pedido.Id);
            if (existingPedido == null) return null;

            existingPedido.NomeCliente = pedido.NomeCliente;
            existingPedido.EmailCliente = pedido.EmailCliente;
            existingPedido.DataCriacao = pedido.DataCriacao;
            existingPedido.Pago = pedido.Pago;
            existingPedido.ItensPedido = pedido.ItensPedido;

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