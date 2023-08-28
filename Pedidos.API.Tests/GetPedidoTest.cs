using AutoFixture;
using Moq;
using Pedidos.API.Application.UseCases;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;

namespace Pedidos.API.Tests
{
    public class GetPedidoTest
    {
        private readonly Mock<IPedidoRepository> _repository;
        private GetPedidoQueryHandler _handler;
        private CancellationToken _cancellationToken;

        public GetPedidoTest()
        {
            _repository = new Mock<IPedidoRepository>();
            _cancellationToken = CancellationToken.None;
            _handler = new GetPedidoQueryHandler(_repository.Object);
        }

        [Fact]
        public async Task Handle_GetPedido_Success()
        {
            // Arrange
            var fixture = new Fixture(); 
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var expectedPedido = fixture.Create<Pedido>();
            var request = new GetPedidoQuery(expectedPedido.Id);

            _repository.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                       .ReturnsAsync((Guid id) => id == expectedPedido.Id ? expectedPedido : null);

            // Act
            var resultPedido = await _handler.Handle(request, _cancellationToken);

            // Assert
            Assert.NotNull(resultPedido);
            Assert.Equal(expectedPedido.Id, resultPedido.Id);
        }

        [Fact]
        public async Task Handle_GetPedido_ThrowException()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var expectedPedido = fixture.Create<Pedido>();
            var request = new GetPedidoQuery(expectedPedido.Id);

            _repository.Setup(repo => repo.GetAsync(It.IsAny<Guid>()))
                       .ThrowsAsync(new Exception());

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, _cancellationToken));
        }
    }
}