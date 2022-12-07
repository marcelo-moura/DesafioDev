namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IRabbitMQMessageConsumer
    {
        string ReceiveMessage(string queueName);
        string ReceiveExchangeMessage(string exchangeName);
    }
}
