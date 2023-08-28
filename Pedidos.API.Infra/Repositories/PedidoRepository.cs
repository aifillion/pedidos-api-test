using Microsoft.EntityFrameworkCore;
using Pedidos.API.Domain.Entities;
using Pedidos.API.Domain.Interfaces.Repositories;
using Pedidos.API.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.API.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DataContext _dbContext;

        public PedidoRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pedido?> GetAsync(Guid idPedido)
        {
            return await _dbContext.Pedidos.Include(x => x.EstadoDelPedido)
                .Where(x => x.Id == idPedido)
                .FirstOrDefaultAsync();
        }

        public async Task<Pedido> SaveAsync(Pedido pedido)
        {
            await _dbContext.Pedidos.AddAsync(pedido);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return pedido;
        }

        public async Task<Pedido> UpdateAsync(Pedido pedido)
        {
            _dbContext.Pedidos.Update(pedido);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return pedido;
        }
    }
}
