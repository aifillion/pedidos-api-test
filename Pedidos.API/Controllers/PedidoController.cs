using AutoMapper;
using Confluent.Kafka;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.API.Application.Metrics;
using Pedidos.API.Application.Models;
using Pedidos.API.Application.UseCases;
using Pedidos.API.Application.Validators;
using Pedidos.API.Domain.Dtos;
using Pedidos.API.Domain.Entities;
using System.Diagnostics;

namespace Pedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ProducerConfig _configuration;
        private readonly IConfiguration _config;
        private readonly ILogger<PedidoController> _logger;
        private readonly IMapper _mapper;

        public PedidoController(IMediator mediator, ProducerConfig configuration, IConfiguration config, ILogger<PedidoController> logger, IServiceProvider serviceProvider)
        {
            _mediator = mediator;
            _configuration = configuration;
            _config = config;
            _logger = logger;
            _mapper = serviceProvider.CreateScope().ServiceProvider.GetService<IMapper>();
        }

        /// <summary>
        /// Obtiene un Pedido a través de un Identificador único
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Pedido encontrado</response>
        /// <response code="404">Pedido NO encontrado</response>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var timer = Stopwatch.StartNew();

            var queryResponse = await _mediator.Send(new GetPedidoQuery(id));

            timer.Stop();
            ApmMetrics.RecordRequestMetrics("GetPedido", timer.ElapsedMilliseconds);

            if (queryResponse == null)
            {
                _logger.LogInformation("Pedido no encontrado: {Id}", id);
                ApmMetrics.RecordError();
                return NotFound("Pedido no encontrado."); 
            }

            _logger.LogInformation("Pedido encontrado: {Id}", id);

            PedidoDto? pedidoDto = _mapper.Map<Pedido?, PedidoDto?>(queryResponse);
            return Ok(pedidoDto);
        }

        /// <summary>
        /// Genera un pedido nuevo.
        /// </summary>
        /// <param name="pedidoModel"></param>
        /// <returns>Un nuevo Pedido</returns>
        /// <response code="201">Pedido Creado</response>
        /// <response code="400">Parámetros inválidos (deben ser númericos)</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromBody] PedidoModel pedidoModel)
        {
            var timer = Stopwatch.StartNew();

            PedidoModelValidator validator = new PedidoModelValidator();
            ValidationResult result = validator.Validate(pedidoModel);

            if (!result.IsValid)
            {
                _logger.LogInformation("Parámetros inválidos (deben ser númericos)");
                ApmMetrics.RecordError();
                return BadRequest(result.Errors);
            }

            Pedido commandResponse = await _mediator.Send(new SavePedidoCommand
                (pedidoModel));

            string locationUrl = $"/api/pedido/{commandResponse.Id}";

            await PublicarEventoPedidoEnKafka(commandResponse);

            timer.Stop();
            ApmMetrics.RecordRequestMetrics("SavePedido", timer.ElapsedMilliseconds);

            _logger.LogInformation("Pedido creado: {Id}", commandResponse.Id);
            return Created(locationUrl, null);
        }

        private async Task PublicarEventoPedidoEnKafka(Pedido pedido)
        {
            string serializedData = JsonConvert.SerializeObject(pedido, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var topic = _config.GetSection("TopicName").Value;

            using (var producer = new ProducerBuilder<Null, string>(_configuration).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedData });
                producer.Flush(TimeSpan.FromSeconds(10));   
            }

        }
    }
}