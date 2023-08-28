using MediatR;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Application.UseCases
{
    public class GetPedidoQuery : IRequest<Pedido?>
    {
        public readonly Guid IdPedido;

        public GetPedidoQuery(Guid idPedido) => IdPedido = idPedido;
    }
}
