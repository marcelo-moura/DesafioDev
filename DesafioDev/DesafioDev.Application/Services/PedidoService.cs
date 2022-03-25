using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Application.Services
{
    public class PedidoService : ServiceBase<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository,
                             INotificador notificador) : base(pedidoRepository, notificador)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> IniciarPedido(Pedido pedidoEntrada)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorUsuarioId(pedidoEntrada.UsuarioId);
            var pedidoItem = new PedidoItem(pedidoEntrada.Id, "Teste", 10, 20);

            if (pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(pedidoEntrada.UsuarioId);
                pedido.AdicionarItem(pedidoItem);

                await _pedidoRepository.Adicionar(pedido);                
            }

            return null;
        }
    }
}
