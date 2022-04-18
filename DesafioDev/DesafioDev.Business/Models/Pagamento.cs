using DesafioDev.Business.Models.Base;

namespace DesafioDev.Business.Models
{
    public class Pagamento : Entity
    {
        public Guid PedidoId { get; set; }
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public string PaymentMethodId { get; set; }
        public string TokenCard { get; set; }
    }
}
