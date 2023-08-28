using MediatR;
using Pedidos.API.Application.Models;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Application.UseCases
{
    public class SavePedidoCommand : IRequest<Pedido>
    {
        public readonly PedidoModel Pedido;

        public SavePedidoCommand(PedidoModel pedido)
        {
            Pedido = pedido;
        }
    }
}
