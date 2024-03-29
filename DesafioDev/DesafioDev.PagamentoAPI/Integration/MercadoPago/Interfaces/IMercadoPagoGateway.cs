﻿using DesafioDev.PagamentoAPI.Models;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Payment;

namespace DesafioDev.PagamentoAPI.Integration.MercadoPago.Interfaces
{
    public interface IMercadoPagoGateway
    {
        Task<Customer> ObterDadosCliente(string email);    
        Task<Payment> CriarPagamento(Pagamento pagamento, List<PaymentItemRequest> paymentItems, Customer customer);
    }
}
