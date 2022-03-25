using DesafioDev.Business.Models;
using DesafioDev.Infra.InterfacesRepository.Base;

namespace DesafioDev.Infra.InterfacesRepository
{
    public interface IPedidoRepository : IRepositoryBase<Pedido>
    {
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId);
    }
}
