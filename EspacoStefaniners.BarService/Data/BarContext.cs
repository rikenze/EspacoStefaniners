using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using Microsoft.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Data
{
    public class BarContext : DbContext, IBarContext
    {
        public BarContext(DbContextOptions<BarContext> opts) : base(opts)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItensPedido>()
               .HasOne(ip => ip.Pedido)
               .WithMany(p => p.Itens)
               .HasForeignKey(ip => ip.IdPedido)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItensPedido>()
                .HasOne(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(ip => ip.IdProduto)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}