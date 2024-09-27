using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EspacoStefaniners.BarService.Services
{
    public class ItensPedidoService : IItensPedidoService
    {
        private readonly IBarContext _context;

        public ItensPedidoService(IBarContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItensPedido>> GetAllItensPedidoAsync()
        {
            return await _context.ItensPedidos.Include(x => x.Produto).Include(y => y.Pedido).ToListAsync();
        }

        public async Task<ItensPedido?> GetItensPedidoByIdAsync(int id)
        {
            return await _context.ItensPedidos.Include(p => p.Pedido).Include(pr => pr.Produto).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ItensPedido?> GetItensPedidoByIdPedidoAsync(int id)
        {
            return await _context.ItensPedidos.Include(p => p.Pedido).Include(pr => pr.Produto).FirstOrDefaultAsync(x => x.IdPedido == id);
        }

        public async Task<ItensPedido> AddItensPedidoAsync(ItensPedido itensPedido)
        {
            var itens = _context.ItensPedidos.Add(itensPedido);
            await _context.SaveChangesAsync();
            return itensPedido;
        }

        public async Task<ItensPedido> UpdateItensPedidoAsync(int id, ItensPedido itensPedido)
        {
            var itensPedidoExistente = await _context.ItensPedidos.FindAsync(id);
            if (itensPedidoExistente == null) return null;

            itensPedidoExistente.IdPedido = itensPedido.IdPedido;
            itensPedidoExistente.IdProduto = itensPedido.IdProduto;
            itensPedidoExistente.Quantidade = itensPedido.Quantidade;

            itensPedidoExistente.Produto = itensPedido.Produto;

            await _context.SaveChangesAsync();
            return itensPedidoExistente;
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