namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IRabbitMQMessageConsumer
    {
        Task<string> ReceiveMessage(string queueName);
        Task<string> ReceiveExchangeMessage(string exchangeName);
    }
}
