using DesafioDev.Business.Enums;
using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Infra.Data.Repository
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(DesafioDevContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pedido>> ObterListaPorUsuarioId(Guid usuarioId)
        {
            return await Db.Pedidos.AsNoTracking().Where(p => p.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoComItensPorId(Guid pedidoId)
        {
            return await Db.Pedidos.AsNoTracking()
                                   .Include(p => p.PedidoItems)
                                   .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }

        public async Task<Pedido> ObterPedidoRascunhoPorUsuarioId(Guid usuarioId)
        {
            var pedido = await Db.Pedidos
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.PedidoStatus == EPedidoStatus.Rascunho);

            if (pedido == null) return null;

            await Db.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            return pedido;
        }
    }
}
