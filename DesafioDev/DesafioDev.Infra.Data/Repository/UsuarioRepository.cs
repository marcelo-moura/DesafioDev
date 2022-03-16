using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Infra.Data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DesafioDevContext context) : base(context)
        {
        }        
    }
}
