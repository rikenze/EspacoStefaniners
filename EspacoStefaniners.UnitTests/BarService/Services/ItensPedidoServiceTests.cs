using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using EspacoStefaniners.BarService.Services;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EspacoStefaniners.BarService.Tests
{
    public class ItensPedidoServiceTests
    {
        private readonly Mock<IBarContext> _mockContext;
        private readonly IItensPedidoRepository _itensPedidoRepository;
        private readonly IItensPedidoService _itensPedidoService;

        public ItensPedidoServiceTests()
        {
            _mockContext = new Mock<IBarContext>();
            _itensPedidoRepository = new ItensPedidoRepository(this._mockContext.Object);
            _itensPedidoService = new ItensPedidoService(_itensPedidoRepository);
        }

        [Fact]
        public async Task GetAllItensPedidoAsync_DeveRetornarTodosItensPedido()
        {
            // Arrange
            var itensPedido = new List<ItemPedido>
            {
                new ItemPedido { Id = 1, Produto = new Produto { NomeProduto = "Cerveja" }, Pedido = new Pedido() },
                new ItemPedido { Id = 2, Produto = new Produto { NomeProduto = "Vinho" }, Pedido = new Pedido() }
            };

            _mockContext.Setup(c => c.ItensPedidos).ReturnsDbSet(itensPedido);

            // Act
            var result = await _itensPedidoService.GetAllItensPedidoAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetItensPedidoByIdAsync_DeveRetornarItemPedido_CasoItemPedidoExista()
        {
            // Arrange
            var itemId = 1;
            var itemPedido = new ItemPedido { Id = itemId, Produto = new Produto { NomeProduto = "Cerveja" }, Pedido = new Pedido() };
            _mockContext.Setup(c => c.ItensPedidos).ReturnsDbSet(new List<ItemPedido> { itemPedido });

            // Act
            var result = await _itensPedidoService.GetItensPedidoByIdAsync(itemId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itemId, result.Id);
        }

        [Fact]
        public async Task GetItensPedidoByIdPedidoAsync_DeveRetornarItemPedido_CasoItemPedidoExista()
        {
            // Arrange
            var pedidoId = 1;
            var itemPedido = new ItemPedido { IdPedido = pedidoId, Produto = new Produto { NomeProduto = "Cerveja" }, Pedido = new Pedido() };
            _mockContext.Setup(c => c.ItensPedidos).ReturnsDbSet(new List<ItemPedido> { itemPedido });

            // Act
            var result = await _itensPedidoService.GetItensPedidoByIdPedidoAsync(pedidoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoId, result.IdPedido);
        }

        [Fact]
        public async Task AddItensPedidoAsync_DeveAdicionarItensPedido()
        {
            // Arrange
            var itensPedido = new List<ItemPedido>
            {
                new ItemPedido { Id = 1, Produto = new Produto { NomeProduto = "Cerveja" }, Pedido = new Pedido() }
            };

            _mockContext.Setup(x => x.ItensPedidos).Returns(new Mock<DbSet<ItemPedido>>().Object);

            // Act
            var result = await _itensPedidoService.AddItensPedidoAsync(itensPedido);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _mockContext.Verify(c => c.ItensPedidos.Add(It.IsAny<ItemPedido>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateItensPedidoAsync_DeveAtualizarItemPedido_CasoItemPedidoExista()
        {
            // Arrange
            var itemId = 1;
            var itemPedidoExistente = new ItemPedido { Id = itemId, Produto = new Produto { NomeProduto = "Cerveja" }, Pedido = new Pedido(), Quantidade = 1 };
            var itemPedidoAtualizado = new ItemPedido { Id = itemId, Produto = new Produto { NomeProduto = "Vinho" }, Pedido = new Pedido(), Quantidade = 2 };

            _mockContext.Setup(c => c.ItensPedidos.FindAsync(itemId)).ReturnsAsync(itemPedidoExistente);

            // Act
            var result = await _itensPedidoService.UpdateItensPedidoAsync(itemId, itemPedidoAtualizado);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(itemPedidoAtualizado.Produto.NomeProduto, result.Produto.NomeProduto);
            Assert.Equal(itemPedidoAtualizado.Quantidade, result.Quantidade);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteItensPedidoAsync_DeveRetornarTrue_CasoItemPedidoExista()
        {
            // Arrange
            var itemId = 1;
            var itemPedido = new ItemPedido { Id = itemId, Produto = new Produto { NomeProduto = "Cerveja" }, Pedido = new Pedido() };
            _mockContext.Setup(c => c.ItensPedidos.FindAsync(itemId)).ReturnsAsync(itemPedido);

            // Act
            var result = await _itensPedidoService.DeleteItensPedidoAsync(itemId);

            // Assert
            Assert.True(result);
            _mockContext.Verify(c => c.ItensPedidos.Remove(itemPedido), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteItensPedidoAsync_DeveRetornarFalso_CasoItemPedidoNaoExista()
        {
            // Arrange
            var itemId = 1;
            _mockContext.Setup(c => c.ItensPedidos.FindAsync(itemId)).ReturnsAsync((ItemPedido)null);

            // Act
            var result = await _itensPedidoService.DeleteItensPedidoAsync(itemId);

            // Assert
            Assert.False(result);
        }
    }
}
