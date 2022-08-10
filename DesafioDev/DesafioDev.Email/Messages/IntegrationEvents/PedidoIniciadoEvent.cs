namespace DesafioDev.Email.Messages.IntegrationEvents
{
    public class PedidoIniciadoEvent : BaseMessage
    {
        public Guid PedidoId { get; set; }
        public string ClienteLogin { get; set; }
        public decimal Total { get; set; }
    }
}
