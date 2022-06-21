using DesafioDev.Core;
using DesafioDev.Infra.Integration.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioDev.Infra.Integration.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private readonly IConfiguration _configuration;

        public RabbitMQMessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostName = configuration["RabbitMQ:HostName"];
            _password = configuration["RabbitMQ:Password"];
            _userName = configuration["RabbitMQ:Username"];
        }

        public void SendMessage<T>(T message, string queueName) where T : BaseMessage
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();

                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

                var body = GetMessageAsByteArray(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }

        private byte[] GetMessageAsByteArray<T>(T message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize<T>(message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }
    }
}
