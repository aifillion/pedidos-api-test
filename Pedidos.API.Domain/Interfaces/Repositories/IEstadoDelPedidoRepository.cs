using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Domain.Interfaces.Repositories
{
    public interface IEstadoDelPedidoRepository
    {
        Task<EstadoDelPedido?> GetAsync(int idPedido); 
    }
}
