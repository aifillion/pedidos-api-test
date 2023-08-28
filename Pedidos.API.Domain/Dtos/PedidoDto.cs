using AutoMapper;
using Pedidos.API.Domain.Entities;

namespace Pedidos.API.Domain.Dtos
{
    public class PedidoDto
    {
        public Guid Id { get; set; }

        public int? NumeroPedido { get; set; }

        public string CicloDelPedido { get; set; } = string.Empty;

        public long CodigoDeContratoInterno { get; set; }

        public string EstadoDelPedido { get; set; }

        public string CuentaCorriente { get; set; } = string.Empty;

        public string Cuando { get; set; }

        public class PedidoProfile : Profile
        {
            public PedidoProfile()
            {
                CreateMap<Pedido, PedidoDto>()
                    .ForMember(x => x.EstadoDelPedido, o => o.MapFrom(y => y.EstadoDelPedido.Descripcion))
                    .ForMember(x => x.Cuando, o => o.MapFrom(y => y.Cuando.ToShortDateString()));
            }
        }
    }
}
