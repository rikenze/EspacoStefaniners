using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EspacoStefaniners.IntegrationTests.BarService.Controllers
{
    public class BebidasIntegrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly BarContext _context;

        public BebidasIntegrationTests()
        {
            // Configura a injeção de configuração para testes
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            var options = new DbContextOptionsBuilder<BarContext>()
            .UseSqlite(_configuration.GetConnectionString("BarConnection"))
            .Options;

            _context = new BarContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetBebidaPorIdAsync_RetornaBebidaSelecionada_DeveRetornarABebida()
        {
            IProdutoService bebidasService = new ProdutoService(_context);

            var cerveja = new Produto
            {
                Id = 1,
                NomeProduto = "Heineken",
                Valor = 12
            };

            var produto = await bebidasService.GetProdutoPorIdAsync(cerveja.Id);

            if (produto == null)
            {
                await bebidasService.AddProduto(cerveja);
            }

            var bebida = await bebidasService.GetProdutoPorIdAsync(cerveja.Id);
            Assert.NotNull(bebida);
        }
    }
}