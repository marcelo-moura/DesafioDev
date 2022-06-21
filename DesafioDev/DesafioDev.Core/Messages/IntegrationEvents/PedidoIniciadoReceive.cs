using NerdStore.Core.DomainObjects.DTO;

namespace DesafioDev.Core.Messages.IntegrationEvents
{
    public class PedidoIniciadoReceive : BaseMessage
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public string ClienteLogin { get; set; }
        public decimal Total { get; set; }
        public ListaProdutosPedido ProdutosPedido { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
        public int Parcelas { get; set; }
        public string TokenCard { get; set; }
        public string PaymentMethodId { get; set; }
    }
}
