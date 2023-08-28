using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Pedidos.API.Application.UseCases;
using Pedidos.API.Application.Validators;
using Pedidos.API.Domain.Dtos;
using Pedidos.API.Domain.Interfaces.Repositories;
using Pedidos.API.Infra.Context;
using Pedidos.API.Infra.Repositories;

namespace Pedidos.API
{
    public class SystemInitializer
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            //Automapper
            builder.Services.AddAutoMapper(typeof(PedidoDto).Assembly);

            //Entity Fwk
            builder.Services.AddDbContext<DataContext>(x =>
                x.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

            //Handlers
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetPedidoQueryHandler).Assembly));

            //Repositories
            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
            builder.Services.AddScoped<IEstadoDelPedidoRepository, EstadoDelPedidoRepository>();

            //Validators
            builder.Services.AddValidatorsFromAssemblyContaining<PedidoModelValidator>();
        }
    }
}
