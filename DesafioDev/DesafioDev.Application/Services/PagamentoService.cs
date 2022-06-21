using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Messages.IntegrationEvents;
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
        private readonly IRabbitMQMessageConsumer _rabbitMQMessageConsumer;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PagamentoService(IPagamentoRepository pagamentoRepository,
                                IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IRabbitMQMessageConsumer rabbitMQMessageConsumer,
                                IMapper mapper,
                                IConfiguration configuration,
                                INotificador notificador) : base(pagamentoRepository, notificador)
        {
            _pagamentoRepository = pagamentoRepository;
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _rabbitMQMessageConsumer = rabbitMQMessageConsumer;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PagamentoViewModelSaida> RealizarPagamentoPedido()
        {
            var pedidoMessage = await _rabbitMQMessageConsumer.ReceiveMessage("iniciar-pedido-queue");

            var pedido = JsonSerializer.Deserialize<PedidoIniciadoReceive>(pedidoMessage);

            var pagamentoRequest = PreencherDadosPagamentoRequest(pedido);

            var pagamento = await _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamentoRequest);

            return new PagamentoViewModelSaida
            {
                PagamentoId = pagamento.Id,
                PedidoId = pedido.PedidoId,
                Total = pagamento.TransactionAmount,
                Status = pagamento.Status
            };
        }

        private Pagamento PreencherDadosPagamentoRequest(PedidoIniciadoReceive? pedido)
        {
            return new Pagamento(pedido.PedidoId, pedido.Total, pedido.Parcelas, pedido.PaymentMethodId, pedido.TokenCard);
        }
    }
}
