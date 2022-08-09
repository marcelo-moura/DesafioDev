using DesafioDev.Email.InterfacesRepository;
using DesafioDev.Email.Messages.IntegrationEvents;
using DesafioDev.Email.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DesafioDev.Email.RabbitMQ
{
    public class RabbitMQPaymentMessageConsumer : BackgroundService
    {
        private readonly IServiceProvider _service;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "FanoutPaymentUpdateExchange";
        string queueName = "";

        public RabbitMQPaymentMessageConsumer(IServiceProvider service, IConfiguration configuration)
        {
            _service = service;
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"],
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
            queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, ExchangeName, "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel, evt) =>
            {
                var body = evt.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<AtualizarPagamentoPedidoEvent>(content);
                ProcessarEmailLogs(message).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(queueName, true, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarEmailLogs(AtualizarPagamentoPedidoEvent message)
        {
            try
            {
                var email = new EmailLog(message.Email, $"Pedido - {message.PedidoId} criado com sucesso");

                using (var scope = _service.CreateScope())
                {
                    var _emailLogRepository = scope.ServiceProvider.GetRequiredService<IEmailLogRepository>();
                    await _emailLogRepository.Adicionar(email);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
