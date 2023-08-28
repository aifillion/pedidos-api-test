using MediatR;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;

namespace Pedidos.API.Application.UseCases
{
    public class GetPedidoQueryHandler : IRequestHandler<GetPedidoQuery, Pedido?>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public GetPedidoQueryHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido?> Handle(GetPedidoQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.GetAsync(request.IdPedido);
            return pedido;
        }
    }
}
