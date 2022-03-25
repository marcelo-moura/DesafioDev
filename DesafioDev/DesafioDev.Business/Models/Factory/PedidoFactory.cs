namespace DesafioDev.Business.Models.Factory
{
    public static class PedidoFactory
    {
        public static Pedido NovoPedidoRascunho(Guid usuarioId)
        {
            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
            };

            pedido.SetStatusPedido(EPedidoStatus.Rascunho);
            return pedido;
        }
    }
}
