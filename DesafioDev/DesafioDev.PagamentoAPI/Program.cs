using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.Integration.RabbitMQ;
using DesafioDev.PagamentoAPI.Context;
using DesafioDev.PagamentoAPI.Integration;
using DesafioDev.PagamentoAPI.Integration.Interfaces;
using DesafioDev.PagamentoAPI.Integration.MercadoPago;
using DesafioDev.PagamentoAPI.Integration.MercadoPago.Interfaces;
using DesafioDev.PagamentoAPI.InterfacesRepository;
using DesafioDev.PagamentoAPI.RabbitMQ;
using DesafioDev.PagamentoAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DesafioDevPagamentoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var build = new DbContextOptionsBuilder<DesafioDevPagamentoContext>();
build.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(new PagamentoRepository(build.Options));
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IMercadoPagoGateway, MercadoPagoGateway>();
builder.Services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
builder.Services.AddScoped<IRabbitMQMessageSender, RabbitMQMessageSender>();

//builder.Services.AddHostedService<RabbitMQPaymentMessageConsumerFanout>();
builder.Services.AddHostedService<RabbitMQPedidoIniciadoMessageConsumerDirect>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
