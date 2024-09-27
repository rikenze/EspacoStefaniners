using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Services
{
    public class ItensPedidoService : IItensPedidoService
    {
        private readonly IBarContext _context;

        public ItensPedidoService(IBarContext context)
        {
            _context = context;
        }

        public async Task<List<ItemPedido>> GetAllItensPedidoAsync()
        {
            return await _context.ItensPedidos.Include(x => x.Produto).Include(y => y.Pedido).ToListAsync();
        }

        public async Task<ItemPedido?> GetItensPedidoByIdAsync(int id)
        {
            return await _context.ItensPedidos.Include(p => p.Pedido).Include(pr => pr.Produto).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ItemPedido?> GetItensPedidoByIdPedidoAsync(int id)
        {
            return await _context.ItensPedidos.Include(p => p.Pedido).Include(pr => pr.Produto).FirstOrDefaultAsync(x => x.IdPedido == id);
        }

        public async Task<List<ItemPedido>> AddItensPedidoAsync(List<ItemPedido> itensPedido)
        {
            foreach (var item in itensPedido)
            {
                var itens = _context.ItensPedidos.Add(item);
            }
            await _context.SaveChangesAsync();
            return itensPedido;
        }

        public async Task<ItemPedido> UpdateItensPedidoAsync(int id, ItemPedido itemPedido)
        {
            var itemPedidoExistente = await _context.ItensPedidos.FindAsync(id);
            if (itemPedidoExistente == null) return null;

            if (itemPedidoExistente != null)
            {
                itemPedidoExistente.Pedido = itemPedido.Pedido;
                itemPedidoExistente.Produto = itemPedido.Produto;
                itemPedidoExistente.Quantidade = itemPedido.Quantidade;
            }

            await _context.SaveChangesAsync();
            return itemPedidoExistente;
        }

        public async Task<bool> DeleteItensPedidoAsync(int id)
        {
            var itensPedido = await _context.ItensPedidos.FindAsync(id);
            if (itensPedido == null) return false;

            _context.ItensPedidos.Remove(itensPedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}