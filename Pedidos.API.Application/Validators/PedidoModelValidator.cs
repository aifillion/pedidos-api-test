using FluentValidation;
using Pedidos.API.Application.Models;

namespace Pedidos.API.Application.Validators
{
    public class PedidoModelValidator : AbstractValidator<PedidoModel>
    {
        public PedidoModelValidator()
        {
            RuleFor(pedido => pedido.CuentaCorriente).NotEmpty().Matches("^[0-9]+$");
            RuleFor(pedido => pedido.CodigoDeContratoInterno).NotEmpty().Matches("^[0-9]+$");
        }
    }
}
