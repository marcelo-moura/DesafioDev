using DesafioDev.Core;

namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage<T>(T message, string queueName) where T : BaseMessage;
        void SendFanoutExchangeMessage<T>(T message, string exchangeName) where T : BaseMessage;
        void SendDirectExchangeMessage<T>(T message, string exchangeName, Dictionary<string, string> queueNameRoutingKey) where T : BaseMessage;
    }
}
