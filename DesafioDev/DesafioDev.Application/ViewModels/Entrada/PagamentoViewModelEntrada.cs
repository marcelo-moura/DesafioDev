namespace DesafioDev.Application.ViewModels.Entrada
{
    public class PagamentoViewModelEntrada
    {
        public Guid PedidoId { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public string PaymentMethodId { get; set; }
        public string TokenCard { get; set; }
    }
}