using Confluent.Kafka;
using MediatR;
using Pedidos.API.Application.UseCases;
using Pedidos.API.Domain.Entities;

namespace Pedidos.WorkerService
{
    public class Worker : BackgroundService
    {

        private readonly IMediator _mediator;
        private readonly ILogger<Worker> _logger;
        private readonly string bootstrapServers = "134.209.172.61:29093"; // "localhost:29092"; 
        private readonly string topic = "PedidoAsignado";
        private readonly string GroupId = "Pedido";

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _mediator = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await ConsumirPedidoAsignado();
                await Task.Delay(1000, stoppingToken);
            }
        }

        protected async Task ConsumirPedidoAsignado()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                GroupId = GroupId
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);
                            if (consumer != null)
                            {
                                string[] detallePedido = consumer.Message.Value.Split("/");
                                await _mediator.Send(new AsignarNumeroPedidoCommand(detallePedido[0], detallePedido[1]));
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            await Task.CompletedTask;
        }
    }
}