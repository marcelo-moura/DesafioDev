using DesafioDev.Core;

namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage<T>(T message, string queueName) where T : BaseMessage;
    }
}
