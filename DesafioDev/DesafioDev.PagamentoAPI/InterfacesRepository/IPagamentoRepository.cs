using DesafioDev.PagamentoAPI.InterfacesRepository.Base;
using DesafioDev.PagamentoAPI.Models;

namespace DesafioDev.PagamentoAPI.InterfacesRepository
{
    public interface IPagamentoRepository : IRepositoryBase<Pagamento>
    {
        Task<bool> AdicionarTransacao(Transacao transacao);
    }
}
