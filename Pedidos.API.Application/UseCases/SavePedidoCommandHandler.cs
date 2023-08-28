using MediatR;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;
using System;

namespace Pedidos.API.Application.UseCases
{
    public class SavePedidoCommandHandler : IRequestHandler<SavePedidoCommand, Pedido>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IEstadoDelPedidoRepository _estadoDelPedidoRepository;

        public SavePedidoCommandHandler(IPedidoRepository pedidoRepository, IEstadoDelPedidoRepository estadoDelPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _estadoDelPedidoRepository = estadoDelPedidoRepository;
        }

        public async Task<Pedido> Handle(SavePedidoCommand request, CancellationToken cancellationToken)
        {
            Guid newGuid = Guid.NewGuid();

            EstadoDelPedido? estadoDelPedido = await _estadoDelPedidoRepository.GetAsync(1);

            return await _pedidoRepository.SaveAsync(new Pedido
            {
                Id = newGuid,
                NumeroPedido = null,
                CicloDelPedido = newGuid.ToString(),
                CodigoDeContratoInterno = long.Parse(request.Pedido.CodigoDeContratoInterno),
                EstadoDelPedido = estadoDelPedido,
                CuentaCorriente = request.Pedido.CuentaCorriente,
                Cuando = DateTime.Now,
            });
        }

    }
}
