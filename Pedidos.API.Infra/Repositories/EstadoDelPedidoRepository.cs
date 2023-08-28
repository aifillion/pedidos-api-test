using Microsoft.EntityFrameworkCore;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;
using Pedidos.API.Infra.Context;

namespace Pedidos.API.Infra.Repositories
{
    public class EstadoDelPedidoRepository : IEstadoDelPedidoRepository
    {
        private readonly DataContext _dbContext;

        public EstadoDelPedidoRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EstadoDelPedido?> GetAsync(int idPedido)
        {
            return await _dbContext.EstadosDelPedido
                .Where(x => x.Id == idPedido)
                .FirstOrDefaultAsync();
        }
    }
}
