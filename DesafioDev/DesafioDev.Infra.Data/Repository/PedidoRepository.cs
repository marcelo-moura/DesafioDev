using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Infra.Data.Repository
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(DesafioDevContext context) : base(context)
        {
        }

        public Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
