﻿using DesafioDev.Core;
using DesafioDev.Infra.Integration.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DesafioDev.Infra.Integration.RabbitMQ
{
    public class RabbitMQMessageSender : BaseRabbitMQ, IRabbitMQMessageSender
    {
        public RabbitMQMessageSender(IConfiguration configuration) : base(configuration)
        {
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

        public void SendFanoutExchangeMessage<T>(T message, string exchangeName) where T : BaseMessage
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();

                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, durable: false);

                var body = GetMessageAsByteArray(message);

                channel.BasicPublish(exchange: exchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public void SendDirectExchangeMessage<T>(T message, string exchangeName, Dictionary<string, string> queueNameRoutingKey) where T : BaseMessage
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: false);

                var body = GetMessageAsByteArray(message);

                foreach (var queueRoutingKey in queueNameRoutingKey)
                {
                    channel.QueueDeclare(queueRoutingKey.Key, false, false, false, null);
                    channel.QueueBind(queueRoutingKey.Key, exchangeName, queueRoutingKey.Value);

                    channel.BasicPublish(exchange: exchangeName,
                                         routingKey: queueRoutingKey.Value,
                                         basicProperties: null,
                                         body: body);
                };
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
    }
}
