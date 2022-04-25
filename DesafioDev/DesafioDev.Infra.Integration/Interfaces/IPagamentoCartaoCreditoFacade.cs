using DesafioDev.Business.Models;
using MercadoPago.Resource.Payment;

namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Task<Payment> RealizarPagamento(Usuario usuario, Pedido pedido, Pagamento pagamento);
    }
}
