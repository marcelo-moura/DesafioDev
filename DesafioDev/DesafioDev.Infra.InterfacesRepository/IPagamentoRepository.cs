using DesafioDev.Business.Models;
using DesafioDev.Infra.InterfacesRepository.Base;

namespace DesafioDev.Infra.InterfacesRepository
{
    public interface IPagamentoRepository : IRepositoryBase<Pagamento>
    {
        Task<bool> AdicionarTransacao(Transacao transacao);
    }
}
