using Microsoft.EntityFrameworkCore;
using Pedidos.API.Application.UseCases;
using Pedidos.API.Domain.Interfaces.Repositories;
using Pedidos.API.Infra.Context;
using Pedidos.API.Infra.Repositories;
using Pedidos.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<DataContext>(x =>
                x.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Pedidos_DesafioAndreani;Trusted_Connection=True;MultipleActiveResultSets=true"));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AsignarNumeroPedidoCommandHandler).Assembly));
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IEstadoDelPedidoRepository, EstadoDelPedidoRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();