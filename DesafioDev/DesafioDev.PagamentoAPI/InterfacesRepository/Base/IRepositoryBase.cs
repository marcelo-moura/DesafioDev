using DesafioDev.PagamentoAPI.Models.Base;

namespace DesafioDev.PagamentoAPI.InterfacesRepository.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<bool> Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
    }
}
