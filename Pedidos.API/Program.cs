using Confluent.Kafka;
using Elastic.Apm.NetCoreAll;
using Microsoft.OpenApi.Models;
using Pedidos.API;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Configure Serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
var producerConfiguration = new ProducerConfig();
builder.Configuration.Bind("producerconfiguration", producerConfiguration);

builder.Services.AddSingleton(producerConfiguration);

builder.Services.AddControllers();

#region CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ARTCorsPolicy", builder =>
    {
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
        //    .AllowAnyHeader()
        //    .AllowAnyMethod();
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((hosts) => true);
    });
});
#endregion

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Pedidos API",
        Description = "API para obtener y crear pedidos."
    });
        
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

SystemInitializer.ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pedidos API V1");
    });
}

app.UseHttpsRedirection();

app.UseCors("ARTCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.UseAllElasticApm(builder.Configuration);

app.Run();