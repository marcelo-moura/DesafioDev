using DesafioDev.Business.Models;
using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.Integration.MercadoPago.Interfaces;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;

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

        public async Task<Payment> RealizarPagamento(Usuario usuario, Pedido pedido, Pagamento pagamento)
        {
            var paymentItems = new List<PaymentItemRequest>();
            foreach (var item in pedido.PedidoItems)
            {
                var paymentItem = new PaymentItemRequest
                {
                    CategoryId = item.Produto.CategoriaId.ToString(),
                    Quantity = item.Quantidade,
                    UnitPrice = item.ValorUnitario
                };
                paymentItems.Add(paymentItem);
            }

            var paymentPayerRequest = await _mercadoPagoGateway.ObterDadosCliente(usuario.Login);
            var paymentResponse = await _mercadoPagoGateway.CriarPagamento(pagamento, paymentItems, paymentPayerRequest);

            return paymentResponse;
        }
    }
}
