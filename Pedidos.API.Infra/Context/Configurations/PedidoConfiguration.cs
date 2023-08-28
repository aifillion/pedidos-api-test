using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Infra.Context.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Pedidos");

            builder.HasOne(e => e.EstadoDelPedido)
                   .WithMany(i => i.Pedidos)
                   .IsRequired();
        }
    }
}
