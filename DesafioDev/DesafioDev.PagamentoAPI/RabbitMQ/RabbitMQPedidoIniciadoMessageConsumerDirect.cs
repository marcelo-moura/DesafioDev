using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.PagamentoAPI.Enums;
using DesafioDev.PagamentoAPI.Integration.Interfaces;
using DesafioDev.PagamentoAPI.InterfacesRepository;
using DesafioDev.PagamentoAPI.Messages.IntegrationEvents;
using DesafioDev.PagamentoAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DesafioDev.PagamentoAPI.RabbitMQ
{
    public class RabbitMQPedidoIniciadoMessageConsumerDirect : BackgroundService
    {
        private readonly IServiceProvider _service;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectIniciarPedidoExchange";
        private const string IniciarPedidoEmailQueueName = "iniciar-pedido-queue";
        private const string IniciarPedidoEmailRoutingKey = "IniciarPedido";

        public RabbitMQPedidoIniciadoMessageConsumerDirect(IServiceProvider service,
                                                           IConfiguration configuration)
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
                ProcessarPagamento(message).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(IniciarPedidoEmailQueueName, true, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessarPagamento(PedidoIniciadoEvent pedidoMessage)
        {
            try
            {
                var pagamentoRequest = PreencherDadosPagamentoRequest(pedidoMessage);
                pagamentoRequest.SetPaymentMethodId(pedidoMessage.PaymentMethodId);
                pagamentoRequest.SetTokenCard(pedidoMessage.TokenCard);

                Transacao transacao;
                using (var scope = _service.CreateScope())
                {
                    var _pagamentoCartaoCreditoFacade = scope.ServiceProvider.GetRequiredService<IPagamentoCartaoCreditoFacade>();
                    transacao = await _pagamentoCartaoCreditoFacade.RealizarPagamento(pedidoMessage, pagamentoRequest);
                }

                if (transacao.StatusTransacao == EStatusTransacao.Pago)
                {
                    var queueNameRoutingKey = new Dictionary<string, string>();
                    queueNameRoutingKey.Add("atualizar-pagamento-email-queue", "AtualizarPagamentoEmail");

                    var pagamentoRealizadoMessage = new PedidoPagamentoRealizadoEvent(pedidoMessage.PedidoId, pedidoMessage.ClienteId, pedidoMessage.ClienteLogin,
                                                                                      transacao.PagamentoId, transacao.Id,
                                                                                      (int)transacao.StatusTransacao, transacao.Total);

                    using (var scope = _service.CreateScope())
                    {
                        var _rabbitMQMessageSender = scope.ServiceProvider.GetRequiredService<IRabbitMQMessageSender>();
                        _rabbitMQMessageSender.SendDirectExchangeMessage(pagamentoRealizadoMessage, "DirectAtualizarPagamentoExchange", queueNameRoutingKey);
                    }

                    using (var scope = _service.CreateScope())
                    {
                        var _pagamentoRepository = scope.ServiceProvider.GetRequiredService<IPagamentoRepository>();
                        await _pagamentoRepository.Adicionar(pagamentoRequest);
                        await _pagamentoRepository.AdicionarTransacao(transacao);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Pagamento PreencherDadosPagamentoRequest(PedidoIniciadoEvent? pedido)
        {
            return new Pagamento(pedido.PedidoId, pedido.Total, pedido.Parcelas, pedido.NomeCartao,
                                 pedido.NumeroCartao, pedido.ExpiracaoCartao, pedido.CvvCartao);
        }
    }
}
