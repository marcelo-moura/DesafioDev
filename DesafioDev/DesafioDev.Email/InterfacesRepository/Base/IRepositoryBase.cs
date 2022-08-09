using DesafioDev.Email.Models.Base;

namespace DesafioDev.Email.InterfacesRepository.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<bool> Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
    }
}
