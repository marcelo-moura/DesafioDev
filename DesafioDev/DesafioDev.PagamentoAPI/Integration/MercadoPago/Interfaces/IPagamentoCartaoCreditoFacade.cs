using DesafioDev.PagamentoAPI.Models;
using DesafioDev.PagamentoAPI.Messages.IntegrationEvents;

namespace DesafioDev.PagamentoAPI.Integration.Interfaces
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Task<Transacao> RealizarPagamento(PedidoIniciadoEvent pedido, Pagamento pagamento);
    }
}
