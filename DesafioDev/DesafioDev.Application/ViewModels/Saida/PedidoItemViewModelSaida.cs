namespace DesafioDev.Application.ViewModels.Saida
{
    public class PedidoItemViewModelSaida
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
    }
}
