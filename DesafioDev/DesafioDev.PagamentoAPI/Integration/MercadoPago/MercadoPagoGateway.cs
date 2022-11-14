using DesafioDev.PagamentoAPI.Integration.MercadoPago.Interfaces;
using DesafioDev.PagamentoAPI.Models;
using MercadoPago.Client;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Payment;

namespace DesafioDev.PagamentoAPI.Integration.MercadoPago
{
    public class MercadoPagoGateway : IMercadoPagoGateway
    {
        private readonly IConfiguration _configuration;

        public MercadoPagoGateway(IConfiguration configuration)
        {
            _configuration = configuration;
            MercadoPagoConfig.AccessToken = _configuration["MercadoPagoAPI:AccessToken"];
        }

        public async Task<Customer> ObterDadosCliente(string email)
        {
            var customerClient = new CustomerClient();

            var searchRequest = new SearchRequest
            {
                Filters = new Dictionary<string, object>()
            };

            searchRequest.Filters.Add("email", email);

            var searchResponse = await customerClient.SearchAsync(searchRequest);
            var customer = searchResponse.Results.FirstOrDefault(x => x.Email == email);

            return customer;
        }

        public async Task<Payment> CriarPagamento(Pagamento pagamento, List<PaymentItemRequest> paymentItems, Customer customer)
        {
            var paymentRequest = new PaymentCreateRequest
            {
                AdditionalInfo = new PaymentAdditionalInfoRequest
                {
                    Items = paymentItems,
                    Payer = new PaymentAdditionalInfoPayerRequest
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName
                    }
                },
                TransactionAmount = pagamento.Valor,
                Token = pagamento.TokenCard,
                Installments = pagamento.Parcelas,
                PaymentMethodId = pagamento.PaymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email = customer.Email,
                    Type = "customer"
                }
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(paymentRequest);

            return payment;
        }
    }
}
