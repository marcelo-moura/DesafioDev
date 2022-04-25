using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Infra.Data.Repository
{
    public class PagamentoRepository : RepositoryBase<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(DesafioDevContext context) : base(context)
        {
        }       
    }
}
