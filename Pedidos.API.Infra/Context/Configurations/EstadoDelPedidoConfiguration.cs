using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Infra.Context.Configurations
{
    public class EstadoDelPedidoConfiguration : IEntityTypeConfiguration<EstadoDelPedido>
    {
        public void Configure(EntityTypeBuilder<EstadoDelPedido> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("EstadoDelPedido");
        }
    }
}
