using DesafioDev.Business.Models;
using DesafioDev.Core.Messages.IntegrationEvents;

namespace DesafioDev.Infra.Integration.Interfaces
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Task<Transacao> RealizarPagamento(PedidoIniciadoReceive pedido, Pagamento pagamento);
    }
}
