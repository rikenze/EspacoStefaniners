using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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
            return await _context.ItensPedidos.ToListAsync();
        }

        public async Task<ItensPedido> GetItensPedidoByIdAsync(int id)
        {
            return await _context.ItensPedidos.FindAsync(id);
        }

        public async Task<ItensPedido> AddItensPedidoAsync(ItensPedido itensPedido)
        {
            var itens = _context.ItensPedidos.Add(itensPedido);
            await _context.SaveChangesAsync();
            return itensPedido;
        }

        public async Task<ItensPedido> UpdateItensPedidoAsync(int id, ItensPedido itensPedido)
        {
            var existingItensPedido = await _context.ItensPedidos.FindAsync(id);
            if (existingItensPedido == null) return null;

            existingItensPedido.IdPedido = itensPedido.IdPedido;
            existingItensPedido.IdProduto = itensPedido.IdProduto;
            existingItensPedido.Quantidade = itensPedido.Quantidade;

            await _context.SaveChangesAsync();
            return existingItensPedido;
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