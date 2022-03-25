using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Infra.Data.Repository
{
    public class PedidoItemRepository : RepositoryBase<PedidoItem>, IPedidoItemRepository
    {
        public PedidoItemRepository(DesafioDevContext context) : base(context)
        {
        }
    }
}
