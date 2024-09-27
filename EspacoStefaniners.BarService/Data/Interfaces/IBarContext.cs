using EspacoStefaniners.BarService.Models;
using Microsoft.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Data.Interfaces
{
    public interface IBarContext
    {
         DbSet<Produto> Produtos { get; set; }
         DbSet<Pedido> Pedidos { get; set; }
         DbSet<ItemPedido> ItensPedidos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}