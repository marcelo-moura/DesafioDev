using DesafioDev.PagamentoAPI.Enums;
using DesafioDev.PagamentoAPI.Models.Base;

namespace DesafioDev.PagamentoAPI.Models
{
    public class Transacao : EntityBase
    {
        public Guid PedidoId { get; private set; }
        public Guid PagamentoId { get; private set; }
        public decimal Total { get; private set; }
        public EStatusTransacao StatusTransacao { get; private set; }

        public Pagamento Pagamento { get; private set; }

        public Transacao() { }

        public Transacao(Guid pedidoId, Guid pagamentoId, decimal total)
        {
            PedidoId = pedidoId;
            PagamentoId = pagamentoId;
            Total = total;
        }

        public void SetPedidoId(Guid pedidoId) => PedidoId = pedidoId;
        public void SetPagamentoId(Guid pagamentoId) => PagamentoId = pagamentoId;
        public void SetTotal(decimal total) => Total = total;
        public void SetStatusTransacao(EStatusTransacao statusTransacao) => StatusTransacao = statusTransacao;
    }
}
