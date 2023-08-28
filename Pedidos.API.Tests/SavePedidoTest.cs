using Moq;
using Pedidos.API.Application.Models;
using Pedidos.API.Application.UseCases;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;

namespace Pedidos.API.Tests
{
    public class SavePedidoTest
    {
        private readonly Mock<IPedidoRepository> _pedidoRepository;
        private readonly Mock<IEstadoDelPedidoRepository> _estadoDelPedidoRepository;
        private SavePedidoCommandHandler _handler;
        private CancellationToken _cancellationToken;

        public SavePedidoTest()
        {
            _pedidoRepository = new Mock<IPedidoRepository>();
            _estadoDelPedidoRepository = new Mock<IEstadoDelPedidoRepository>();
            _cancellationToken = CancellationToken.None;
            _handler = new SavePedidoCommandHandler(_pedidoRepository.Object, _estadoDelPedidoRepository.Object);
        }

        [Fact]
        public async Task Handle_GetPedido_Success()
        {
            // Arrange
            var pedidoModel = new PedidoModel
            {
                CodigoDeContratoInterno = "123123",
                CuentaCorriente = "123123"
            };

            var request = new SavePedidoCommand(pedidoModel);

            _pedidoRepository.Setup(repo => repo.SaveAsync(It.IsAny<Pedido>()))
                              .ReturnsAsync((Pedido pedido) =>
                              {
                                  pedido.Id = Guid.NewGuid();
                                  return pedido;
                              });

            // Act
            var savedPedido = await _handler.Handle(request, _cancellationToken);

            // Assert
            Assert.NotNull(savedPedido);
            Assert.NotEqual(Guid.Empty, savedPedido.Id);
        }

        [Fact]
        public async Task Handle_GetPedido_ThrowException()
        {
            // Arrange
            var invalidPedidoModel = new PedidoModel();
            var request = new SavePedidoCommand(invalidPedidoModel);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(request, _cancellationToken));
        }
    }
}