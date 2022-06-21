using DesafioDev.Business.Models;
using DesafioDev.Core.Messages.IntegrationEvents;
using MercadoPago.Resource.Payment;

namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Task<Payment> RealizarPagamento(PedidoIniciadoReceive pedido, Pagamento pagamento);
    }
}
