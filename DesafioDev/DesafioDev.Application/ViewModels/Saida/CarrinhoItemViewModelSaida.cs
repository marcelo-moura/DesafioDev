namespace DesafioDev.Application.ViewModels.Saida
{
    public class CarrinhoItemViewModelSaida
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
