﻿using DesafioDev.PagamentoAPI.Enums;
using DesafioDev.PagamentoAPI.Integration.Interfaces;
using DesafioDev.PagamentoAPI.Integration.MercadoPago.Interfaces;
using DesafioDev.PagamentoAPI.Messages.IntegrationEvents;
using DesafioDev.PagamentoAPI.Models;
using MercadoPago.Client.Payment;

namespace DesafioDev.PagamentoAPI.Integration
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

        public async Task<Transacao> RealizarPagamento(PedidoIniciadoEvent pedido, Pagamento pagamento)
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
