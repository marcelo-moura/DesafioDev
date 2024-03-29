﻿using DesafioDev.Business.Models;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Customer;
using MercadoPago.Resource.Payment;

namespace DesafioDev.Infra.Integration.MercadoPago.Interfaces
{
    public interface IMercadoPagoGateway
    {
        Task<Customer> ObterDadosCliente(string email);
        Task<Customer> CriarCliente(Usuario usuario);
        Task<CustomerCard> CriarCartaoCliente(string token, string email);
    }
}
