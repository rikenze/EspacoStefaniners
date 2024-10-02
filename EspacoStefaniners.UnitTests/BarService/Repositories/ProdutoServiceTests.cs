using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EspacoStefaniners.BarService.Tests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IBarContext> _mockContext;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _mockContext = new Mock<IBarContext>();
            _produtoRepository = new ProdutoRepository(_mockContext.Object);
            _produtoService = new ProdutoService(_produtoRepository);
        }

        [Fact]
        public async Task GetProdutoPorIdAsync_DeveRetornarProduto_CasoProdutoExista()
        {
            // Arrange
            var produtoId = 1;
            var produto = new Produto { Id = produtoId, NomeProduto = "Cerveja", Valor = 10 };
            _mockContext.Setup(c => c.Produtos.FindAsync(produtoId)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.GetProdutoPorIdAsync(produtoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoId, result.Id);
        }

        [Fact]
        public async Task GetAllProdutosAsync_DeveRetornarTodosProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, NomeProduto = "Cerveja", Valor = 10 },
                new Produto { Id = 2, NomeProduto = "Vinho", Valor = 20 }
            };

            _mockContext.Setup(c => c.Produtos).ReturnsDbSet(produtos);

            // Act
            var result = await _produtoService.GetAllProdutosAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddProduto_DeveRetornarNull_CasoProdutoJaExista()
        {
            // Arrange
            var produto = new Produto { Id = 1, NomeProduto = "Cerveja", Valor = 10 };
            _mockContext.Setup(c => c.Produtos.FindAsync(produto.Id)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.AddProduto(produto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddProduto_DeveAdicionarProduto_CasoProdutoNaoExista()
        {
            // Arrange
            var produto = new Produto { Id = 1, NomeProduto = "Cerveja", Valor = 10 };
            _mockContext.Setup(c => c.Produtos.FindAsync(produto.Id)).ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.AddProduto(produto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Id, result.Id);
            _mockContext.Verify(c => c.Produtos.AddAsync(produto, default), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task AtualizarProdutoAsync_DeveRetornarNull_CasoProdutoNaoExista()
        {
            // Arrange
            var produtoId = 1;
            var produto = new Produto { Id = produtoId, NomeProduto = "Cerveja", Valor = 10 };
            _mockContext.Setup(c => c.Produtos.FindAsync(produtoId)).ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.AtualizarProdutoAsync(produtoId, produto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AtualizarProdutoAsync_DeveAtualizarProduto_CasoProdutoExista()
        {
            // Arrange
            var produtoId = 1;
            var produtoExistente = new Produto { Id = produtoId, NomeProduto = "Cerveja", Valor = 10 };
            var produtoAtualizado = new Produto { Id = produtoId, NomeProduto = "Vinho", Valor = 20 };

            _mockContext.Setup(c => c.Produtos.FindAsync(produtoId)).ReturnsAsync(produtoExistente);

            // Act
            var result = await _produtoService.AtualizarProdutoAsync(produtoId, produtoAtualizado);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoAtualizado.NomeProduto, result.NomeProduto);
            Assert.Equal(produtoAtualizado.Valor, result.Valor);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeletarProdutoAsync_DeveRetornarNull_CasoProdutoNaoExista()
        {
            // Arrange
            var produtoId = 1;
            _mockContext.Setup(c => c.Produtos.FindAsync(produtoId)).ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.DeletarProdutoAsync(produtoId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeletarProdutoAsync_DeveRemoverProduto_CasoProdutoExista()
        {
            // Arrange
            var produtoId = 1;
            var produto = new Produto { Id = produtoId, NomeProduto = "Cerveja", Valor = 10 };
            _mockContext.Setup(c => c.Produtos.FindAsync(produtoId)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.DeletarProdutoAsync(produtoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoId, result.Id);
            _mockContext.Verify(c => c.Produtos.Remove(produto), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
