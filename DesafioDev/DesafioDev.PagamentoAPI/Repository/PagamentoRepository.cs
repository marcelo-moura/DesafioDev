using DesafioDev.PagamentoAPI.Context;
using DesafioDev.PagamentoAPI.InterfacesRepository;
using DesafioDev.PagamentoAPI.Models;
using DesafioDev.PagamentoAPI.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.PagamentoAPI.Repository
{
    public class PagamentoRepository : RepositoryBase<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(DbContextOptions<DesafioDevPagamentoContext> context) : base(context)
        {
        }

        public async Task<bool> AdicionarTransacao(Transacao transacao)
        {
            await DbSet.Transacoes.AddAsync(transacao);
            return await DbSet.SaveChangesAsync() > 0;
        }
    }
}
