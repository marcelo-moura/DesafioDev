using DesafioDev.Email.Enums;

namespace DesafioDev.Email.Messages.IntegrationEvents
{
    public class AtualizarPagamentoPedidoEvent : BaseMessage
    {
        public Guid PedidoId { get; set; }
        public string Email { get; set; }
        public EStatusTransacao StatusTransacao { get; set; }
    }
}
