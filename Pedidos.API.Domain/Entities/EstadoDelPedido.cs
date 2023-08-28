using System.Text.Json.Serialization;

namespace Pedidos.API.Domain.Entities
{
    public class EstadoDelPedido
    {
        public int Id { get; set; }

        public string Descripcion { get; set;}

        [JsonIgnore]
        public virtual List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
