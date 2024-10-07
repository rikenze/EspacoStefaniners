using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace EspacoStefaniners.BarService.Tests
{
    public class PedidoRepositoryTests
    {
        private readonly Mock<IBarContext> _mockContext;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoRepositoryTests()
        {
            _mockContext = new Mock<IBarContext>();
            _pedidoRepository = new PedidoRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllPedidosAsync_DeveRetornarTodosPedidos()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>() },
                new Pedido { Id = 2, NomeCliente = "Cliente 2", ItensPedido = new List<ItemPedido>() }
            };

            _mockContext.Setup(c => c.Pedidos).ReturnsDbSet(pedidos);

            // Act
            var result = await _pedidoRepository.GetAllPedidosAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPedidoByIdAsync_DeveRetornarPedido_CasoPedidoExists()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Pedido { Id = pedidoId, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>() };
            _mockContext.Setup(c => c.Pedidos).ReturnsDbSet(new List<Pedido> { pedido });

            // Act
            var result = await _pedidoRepository.GetPedidoByIdAsync(pedidoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoId, result.Id);
        }

        [Fact]
        public async Task AddPedidoAsync_DeveAdicionarPedido()
        {
            // Arrange
            var pedido = new Pedido { Id = 1, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>() };
            _mockContext.Setup(x => x.Pedidos).ReturnsDbSet(new List<Pedido>() { pedido });

            // Act
            var result = await _pedidoRepository.AddPedidoAsync(pedido);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedido.Id, result.Id);
            _mockContext.Verify(c => c.Pedidos.Add(pedido), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdatePedidoAsync_DeveRetornarNull_CasoPedidoNaoExista()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Pedido { Id = pedidoId, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>() };
            _mockContext.Setup(c => c.Pedidos.FindAsync(pedidoId)).ReturnsAsync((Pedido)null);

            // Act
            var result = await _pedidoRepository.UpdatePedidoAsync(pedido);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePedidoAsync_DeveAtualizarPedido_CasoPedidoExista()
        {
            // Arrange
            var pedidoId = 1;
            var pedidoExistente = new Pedido { Id = pedidoId, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>() };
            var pedidoAtualizado = new Pedido { Id = pedidoId, NomeCliente = "Cliente 2", ItensPedido = new List<ItemPedido>() };

            _mockContext.Setup(c => c.Pedidos.FindAsync(pedidoId)).ReturnsAsync(pedidoExistente);

            // Act
            var result = await _pedidoRepository.UpdatePedidoAsync(pedidoAtualizado);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedidoAtualizado.NomeCliente, result.NomeCliente);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeletePedidoAsync_DeveRetornarTrue_CasoPedidoExista()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Pedido { Id = pedidoId, NomeCliente = "Cliente 1", ItensPedido = new List<ItemPedido>() };
            _mockContext.Setup(c => c.Pedidos.FindAsync(pedidoId)).ReturnsAsync(pedido);

            // Act
            var result = await _pedidoRepository.DeletePedidoAsync(pedidoId);

            // Assert
            Assert.True(result);
            _mockContext.Verify(c => c.Pedidos.Remove(pedido), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeletePedidoAsync_DeveRetornarFalse_CasoPedidoNaoExista()
        {
            // Arrange
            var pedidoId = 1;
            _mockContext.Setup(c => c.Pedidos.FindAsync(pedidoId)).ReturnsAsync((Pedido)null);

            // Act
            var result = await _pedidoRepository.DeletePedidoAsync(pedidoId);

            // Assert
            Assert.False(result);
        }
    }
}
