using MediatR;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;

namespace Pedidos.API.Application.UseCases
{
    public class AsignarNumeroPedidoCommandHandler : IRequestHandler<AsignarNumeroPedidoCommand, Pedido?>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEstadoDelPedidoRepository _estadoDelPedidoRepository;

        public AsignarNumeroPedidoCommandHandler(IPedidoRepository pedidoRepository, IEstadoDelPedidoRepository estadoDelPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _estadoDelPedidoRepository = estadoDelPedidoRepository;
        }

        public async Task<Pedido?> Handle(AsignarNumeroPedidoCommand request, CancellationToken cancellationToken)
        {
            Guid guid = new Guid(request.IdPedido);
            Pedido? pedidoActualizar = await _pedidoRepository.GetAsync(guid);

            EstadoDelPedido? estadoDelPedido = await _estadoDelPedidoRepository.GetAsync(2);

            if (pedidoActualizar != null)
            {
                pedidoActualizar.NumeroPedido = Int32.Parse(request.NumeroPedido);
                pedidoActualizar.EstadoDelPedido = estadoDelPedido;
                return await _pedidoRepository.UpdateAsync(pedidoActualizar);
            }

            return null;
        }
    }
}
