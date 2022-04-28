namespace DesafioDev.Application.ViewModels.Saida
{
    public class PagamentoViewModelSaida
    {
        public Guid PedidoId { get; set; }
        public long? PagamentoId { get; set; }
        public decimal? Total { get; set; }
        public string Status { get; set; }
    }
}