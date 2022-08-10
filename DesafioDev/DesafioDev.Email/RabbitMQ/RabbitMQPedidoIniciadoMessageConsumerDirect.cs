using DesafioDev.Email.InterfacesRepository;
using DesafioDev.Email.Messages.IntegrationEvents;
using DesafioDev.Email.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DesafioDev.Email.RabbitMQ
{
    public class RabbitMQPedidoIniciadoMessageConsumerDirect : BackgroundService
    {
        private readonly IServiceProvider _service;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectIniciarPedidoExchange";
        private const string IniciarPedidoEmailQueueName = "iniciar-pedido-email-queue";
        private const string IniciarPedidoEmailRoutingKey = "IniciarPedidoEmail";

        public RabbitMQPedidoIniciadoMessageConsumerDirect(IServiceProvider service, IConfiguration configuration)
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

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(IniciarPedidoEmailQueueName, false, false, false, null);
            _channel.QueueBind(IniciarPedidoEmailQueueName, ExchangeName, IniciarPedidoEmailRoutingKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (channel, evt) =>
            {
                var body = evt.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<PedidoIniciadoEvent>(content);
                ProcessarEmailLogs(message).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(IniciarPedidoEmailQueueName, true, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarEmailLogs(PedidoIniciadoEvent message)
        {
            try
            {
                var email = new EmailLog(message.ClienteLogin, $"Pedido - {message.PedidoId} criado com sucesso");

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
