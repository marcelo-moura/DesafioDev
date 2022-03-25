using DesafioDev.Business.Models;
using DesafioDev.Infra.InterfacesRepository.Base;

namespace DesafioDev.Infra.InterfacesRepository
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<IEnumerable<Pedido>> ObterListaPorUsuarioId(Guid usuarioId);
        Task<Pedido> ObterPedidoRascunhoPorUsuarioId(Guid usuarioId);
    }
}
