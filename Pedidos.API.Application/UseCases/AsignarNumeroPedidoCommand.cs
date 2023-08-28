using MediatR;
using Pedidos.API.Application.Models;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Application.UseCases
{
    public class AsignarNumeroPedidoCommand : IRequest<Pedido?>
    {
        public readonly string IdPedido;
        public readonly string NumeroPedido;

        public AsignarNumeroPedidoCommand(string idPedido, string numeroPedido)
        {
            IdPedido = idPedido;
            NumeroPedido = numeroPedido;
        }
    }
}
