using DesafioDev.Core;

namespace DesafioDev.PagamentoAPI.Messages.IntegrationEvents
{
    public class PedidoPagamentoRealizadoEvent : BaseMessage
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public string Email { get; private set; }
        public Guid PagamentoId { get; private set; }
        public Guid TransacaoId { get; private set; }
        public int StatusTransacao { get; private set; }
        public decimal Total { get; private set; }

        public PedidoPagamentoRealizadoEvent(Guid pedidoId, Guid clienteId, string email, Guid pagamentoId, Guid transacaoId, int statusTransacao, decimal total)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Email = email;
            PagamentoId = pagamentoId;
            TransacaoId = transacaoId;
            StatusTransacao = statusTransacao;
            Total = total;
        }
    }
}