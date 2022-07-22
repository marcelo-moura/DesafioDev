using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DesafioDev.Infra.Integration.RabbitMQ
{
    public abstract class BaseRabbitMQ
    {
        protected IConnection _connection;
        private readonly IConfiguration _configuration;

        public BaseRabbitMQ(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _configuration["RabbitMQ:HostName"],
                    Password = _configuration["RabbitMQ:Password"],
                    UserName = _configuration["RabbitMQ:Username"]
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }
    }
}
