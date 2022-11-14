using DesafioDev.PagamentoAPI.Models.Base;

namespace DesafioDev.PagamentoAPI.Models
{
    public class Pagamento : EntityBase
    {
        public Guid PedidoId { get; private set; }
        public decimal Valor { get; private set; }
        public int Parcelas { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }
        public string PaymentMethodId { get; private set; }
        public string TokenCard { get; private set; }

        public Transacao Transacao { get; private set; }

        public Pagamento(Guid pedidoId, decimal valor, int parcelas, string nomeCartao, string numeroCartao,
                         string expiracaoCartao, string cvvCartao)
        {
            PedidoId = pedidoId;
            Valor = valor;
            Parcelas = parcelas;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public void SetPaymentMethodId(string paymentMethodId) => PaymentMethodId = paymentMethodId;
        public void SetTokenCard(string tokenCard) => TokenCard = tokenCard;
    }
}
