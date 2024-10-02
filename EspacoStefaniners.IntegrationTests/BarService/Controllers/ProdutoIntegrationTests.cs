using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EspacoStefaniners.IntegrationTests.BarService.Controllers
{
    public class ProdutoIntegrationTests
    {
        private readonly IConfiguration _configuration;
        private readonly BarContext _context;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;

        public ProdutoIntegrationTests()
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

            _produtoRepository = new ProdutoRepository(_context);
            _produtoService = new ProdutoService(_produtoRepository);
        }

        [Fact]
        public async Task GetProdutoPorIdAsync_RetornaProdutoSelecionado_DeveRetornarProduto()
        {
            var produto = new Produto
            {
                Id = 1,
                NomeProduto = "Heineken",
                Valor = 12
            };

            var produtoExistente = await _produtoService.GetProdutoPorIdAsync(produto.Id);

            if (produtoExistente == null)
            {
                await _produtoService.AddProduto(produto);
            }

            Assert.NotNull(produtoExistente);
        }
    }
}