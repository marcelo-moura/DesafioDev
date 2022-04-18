using DesafioDev.Business.Models;
using DesafioDev.Infra.Integration.MercadoPago.Interfaces;
using MercadoPago.Client;
using MercadoPago.Client.Common;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;

namespace DesafioDev.Infra.Integration.MercadoPago
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

        public async Task<Customer> CriarCliente(Usuario usuario)
        {
            var customerClient = new CustomerClient();

            var clienteRequest = new CustomerRequest
            {
                Email = usuario.Login,
                FirstName = usuario.NomeCompleto.Split(" ")[0],
                LastName = usuario.NomeCompleto.Split(" ")[1],
                DateRegistred = DateTime.Now
            };

            var cliente = await customerClient.CreateAsync(clienteRequest);
            return cliente;
        }

        public async Task<CustomerCard> CriarCartaoCliente(string token, string email)
        {
            var customerClient = new CustomerClient();

            var customer = await ObterDadosCliente(email);

            CustomerCard card = null;
            if (customer != null && !string.IsNullOrEmpty(customer.Id))
            {
                var cardRequest = new CustomerCardCreateRequest
                {
                    Token = token
                };

                card = await customerClient.CreateCardAsync(customer.Id, cardRequest);
            }

            return card;
        }

        public async Task<Payment> CriarPagamento(Pagamento pagamento, List<PaymentItemRequest> paymentItems, PaymentPayerRequest payerRequest)
        {
            var paymentRequest = new PaymentCreateRequest
            {
                AdditionalInfo = new PaymentAdditionalInfoRequest
                {
                    Items = paymentItems
                },
                TransactionAmount = pagamento.Valor,
                Token = pagamento.TokenCard,
                Installments = pagamento.Parcelas,
                PaymentMethodId = pagamento.PaymentMethodId,
                Payer = new PaymentPayerRequest
                {
                    Email = payerRequest.Email,
                    Identification = new IdentificationRequest
                    {
                        Type = payerRequest.Identification.Type,
                        Number = payerRequest.Identification.Number
                    },
                    FirstName = payerRequest.FirstName,
                    LastName = payerRequest.LastName
                }
            };

            var client = new PaymentClient();
            Payment payment = await client.CreateAsync(paymentRequest);
            return payment;
        }
    }
}
