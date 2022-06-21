namespace DesafioDev.Application.ViewModels.Saida
{
    public class CarrinhoViewModelSaida
    {
        public Guid PedidoId { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal ValorTotal { get; set; }

        public List<CarrinhoItemViewModelSaida> Items { get; set; } = new List<CarrinhoItemViewModelSaida>();
    }
}
