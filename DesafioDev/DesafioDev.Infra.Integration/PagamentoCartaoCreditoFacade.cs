﻿using DesafioDev.Business.Enums;
using DesafioDev.Business.Models;
using DesafioDev.Core.Messages.IntegrationEvents;
using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.Integration.MercadoPago.Interfaces;
using MercadoPago.Client.Payment;
using Microsoft.Extensions.Configuration;
using NerdStore.Core.DomainObjects.DTO;

namespace DesafioDev.Infra.Integration
{
    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IMercadoPagoGateway _mercadoPagoGateway;
        private readonly IConfiguration _configuration;

        public PagamentoCartaoCreditoFacade(IMercadoPagoGateway mercadoPagoGateway, IConfiguration configuration)
        {
            _mercadoPagoGateway = mercadoPagoGateway;
            _configuration = configuration;
        }

        public async Task<Transacao> RealizarPagamento(PedidoIniciadoReceive pedido, Pagamento pagamento)
        {
            var paymentItems = PreencherPaymentItemsRequest(pedido.ProdutosPedido);

            var paymentPayerRequest = await _mercadoPagoGateway.ObterDadosCliente(pedido.ClienteLogin);
            var paymentResponse = await _mercadoPagoGateway.CriarPagamento(pagamento, paymentItems, paymentPayerRequest);

            var transacao = new Transacao(pedido.PedidoId, pagamento.Id, pedido.Total);

            if (paymentResponse?.Status != null && paymentResponse.Status == "approved")
            {
                transacao.SetStatusTransacao(EStatusTransacao.Pago);
                return transacao;
            }

            transacao.SetStatusTransacao(EStatusTransacao.Recusado);
            return transacao;
        }

        private List<PaymentItemRequest> PreencherPaymentItemsRequest(ListaProdutosPedido produtosPedido)
        {
            var paymentItems = new List<PaymentItemRequest>();
            foreach (var item in produtosPedido.Itens)
            {
                var paymentItem = new PaymentItemRequest
                {
                    Quantity = item.Quantidade,
                    UnitPrice = item.ValorUnitario
                };

                paymentItems.Add(paymentItem);
            }
            return paymentItems;
        }
    }
}
