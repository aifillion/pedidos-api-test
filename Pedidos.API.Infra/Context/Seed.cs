using Microsoft.EntityFrameworkCore;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Infra.Context
{
    public class Seed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstadoDelPedido>().HasData(new EstadoDelPedido { Id = 1, Descripcion = "CREADO" });
            modelBuilder.Entity<EstadoDelPedido>().HasData(new EstadoDelPedido { Id = 2, Descripcion = "ASIGNADO" });
            modelBuilder.Entity<EstadoDelPedido>().HasData(new EstadoDelPedido { Id = 3, Descripcion = "CERRADO" });
            modelBuilder.Entity<EstadoDelPedido>().HasData(new EstadoDelPedido { Id = 4, Descripcion = "RECHAZADO" });
        }
    }
}
