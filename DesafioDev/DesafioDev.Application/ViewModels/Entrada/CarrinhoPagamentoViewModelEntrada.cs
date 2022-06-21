namespace DesafioDev.Application.ViewModels.Entrada
{
    public class CarrinhoPagamentoViewModelEntrada
    {
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
        public int Parcelas { get; set; }
        public string TokenCard { get; set; }
        public string PaymentMethodId { get; set; }
    }
}
