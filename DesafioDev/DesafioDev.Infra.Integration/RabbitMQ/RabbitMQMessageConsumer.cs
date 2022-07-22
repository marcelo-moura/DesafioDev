using DesafioDev.Infra.Integration.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DesafioDev.Infra.Integration.RabbitMQ
{
    public class RabbitMQMessageConsumer : BaseRabbitMQ, IRabbitMQMessageConsumer
    {
        private IModel _channel;

        public RabbitMQMessageConsumer(IConfiguration configuration) : base(configuration)
        {
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

        public async Task<string> ReceiveExchangeMessage(string exchangeName)
        {
            string content = string.Empty;

            if (ConnectionExists())
            {
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

                var queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queueName, exchangeName, "");

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
    }
}
