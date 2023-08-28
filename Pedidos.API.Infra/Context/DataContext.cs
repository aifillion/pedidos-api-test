using Microsoft.EntityFrameworkCore;
using Pedidos.API.Domain.Entities;
using System.Reflection;

namespace Pedidos.API.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<EstadoDelPedido> EstadosDelPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Seed.SeedData(modelBuilder);
        }
    }
}
