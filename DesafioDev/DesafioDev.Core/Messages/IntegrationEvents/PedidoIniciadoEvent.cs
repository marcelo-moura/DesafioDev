using NerdStore.Core.DomainObjects.DTO;

namespace DesafioDev.Core.Messages.IntegrationEvents
{
    public class PedidoIniciadoEvent : BaseMessage
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public string ClienteLogin { get; private set; }
        public decimal Total { get; private set; }
        public ListaProdutosPedido ProdutosPedido { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }
        public int Parcelas { get; private set; }
        public string TokenCard { get; private set; }
        public string PaymentMethodId { get; private set; }

        public PedidoIniciadoEvent(Guid pedidoId, Guid clienteId, string clienteLogin, decimal total, ListaProdutosPedido itens, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao, int parcelas, string tokenCard, string paymentMethodId)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ClienteLogin = clienteLogin;
            Total = total;
            ProdutosPedido = itens;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
            Parcelas = parcelas;
            TokenCard = tokenCard;
            PaymentMethodId = paymentMethodId;
        }
    }
}
