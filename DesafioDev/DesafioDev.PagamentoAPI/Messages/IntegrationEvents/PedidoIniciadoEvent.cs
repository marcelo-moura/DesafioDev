using DesafioDev.Core;

namespace DesafioDev.PagamentoAPI.Messages.IntegrationEvents
{
    public class PedidoIniciadoEvent : BaseMessage
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

    public class ListaProdutosPedido
    {
        public Guid PedidoId { get; set; }
        public ICollection<Item> Itens { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
