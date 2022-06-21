using DesafioDev.Infra.Integration.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DesafioDev.Infra.Integration.RabbitMQSender
{
    public class RabbitMQMessageConsumer : IRabbitMQMessageConsumer
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IConfiguration _configuration;

        public RabbitMQMessageConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> ReceiveMessage(string queueName)
        {
            string content = string.Empty;

            if (ConnectionExists())
            {
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (channel, evt) =>
                {
                    var body = evt.Body.ToArray();
                    content = Encoding.UTF8.GetString(body);
                    _channel.BasicAck(evt.DeliveryTag, false);
                };
                _channel.BasicConsume(queueName, true, consumer);
            }
            return await Task.FromResult(content);
        }

        private void CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Password = _configuration["RabbitMQ:Password"],
                UserName = _configuration["RabbitMQ:Username"],
            };
            _connection = factory.CreateConnection();
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }
    }
}
