using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.Interfaces
{
    public interface IPedidoService : IServiceBase<Pedido>
    {
        Task<Pedido> IniciarPedido(Pedido pedido);
    }
}
