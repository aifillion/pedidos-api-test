using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido?> GetAsync(Guid idPedido); 
        Task<Pedido> SaveAsync(Pedido pedido);
        Task<Pedido> UpdateAsync(Pedido pedido);
    }
}
