using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Infra.Data.Repository
{
    public class PagamentoRepository : RepositoryBase<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(DesafioDevContext context) : base(context)
        {
        }

        public async Task<bool> AdicionarTransacao(Transacao transacao)
        {
            await Db.Transacoes.AddAsync(transacao);
            return await Db.SaveChangesAsync() > 0;
        }
    }
}
