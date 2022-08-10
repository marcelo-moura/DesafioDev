using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Enums;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Messages.IntegrationEvents;
using DesafioDev.Infra.Common.Utils;
using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace DesafioDev.Application.Services
{
    public class PagamentoService : ServiceBase<Pagamento>, IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IRabbitMQMessageConsumer _rabbitMQMessageConsumer;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PagamentoService(IPagamentoRepository pagamentoRepository,
                                IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IRabbitMQMessageSender rabbitMQMessageSender,
                                IRabbitMQMessageConsumer rabbitMQMessageConsumer,
                                IMapper mapper,
                                IConfiguration configuration,
                                INotificador notificador) : base(pagamentoRepository, notificador)
        {
            _pagamentoRepository = pagamentoRepository;
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _rabbitMQMessageConsumer = rabbitMQMessageConsumer;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PagamentoViewModelSaida> RealizarPagamentoPedido()
        {
            var pedidoMessage = await _rabbitMQMessageConsumer.ReceiveMessage("iniciar-pedido-queue");

            var pedido = JsonSerializer.Deserialize<PedidoIniciadoReceive>(pedidoMessage);

            var pagamentoRequest = PreencherDadosPagamentoRequest(pedido);
            pagamentoRequest.SetPaymentMethodId(pedido.PaymentMethodId);
            pagamentoRequest.SetTokenCard(pedido.TokenCard);

            var transacao = await _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamentoRequest);

            if (transacao.StatusTransacao == EStatusTransacao.Pago)
            {
                var queueNameRoutingKey = new Dictionary<string, string>();
                queueNameRoutingKey.Add("atualizar-pagamento-email-queue", "AtualizarPagamentoEmail");

                var pagamentoRealizadoMessage = new PedidoPagamentoRealizadoEvent(pedido.PedidoId, pedido.ClienteId, pedido.ClienteLogin,
                                                                                  transacao.PagamentoId, transacao.Id,
                                                                                  (int)transacao.StatusTransacao, transacao.Total);

                _rabbitMQMessageSender.SendDirectExchangeMessage(pagamentoRealizadoMessage, "DirectAtualizarPagamentoExchange", queueNameRoutingKey);

                await _pagamentoRepository.Adicionar(pagamentoRequest);
                await _pagamentoRepository.AdicionarTransacao(transacao);
            }

            return new PagamentoViewModelSaida
            {
                PagamentoId = transacao.PagamentoId,
                PedidoId = transacao.PedidoId,
                Total = transacao.Total,
                Status = transacao.StatusTransacao.GetDescription()
            };
        }

        private Pagamento PreencherDadosPagamentoRequest(PedidoIniciadoReceive? pedido)
        {
            return new Pagamento(pedido.PedidoId, pedido.Total, pedido.Parcelas, pedido.NomeCartao,
                                 pedido.NumeroCartao, pedido.ExpiracaoCartao, pedido.CvvCartao);
        }
    }
}
