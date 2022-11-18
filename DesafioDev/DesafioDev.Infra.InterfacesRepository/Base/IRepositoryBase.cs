using DesafioDev.Business.Models.Base;
using System.Linq.Expressions;

namespace DesafioDev.Infra.InterfacesRepository.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task<bool> Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IList<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> BuscarPor(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntityCustom>> BuscarComPagedSearch<TEntityCustom>(string nameProcedure, int sortOrder, string sortDirection) where TEntityCustom : class;
        Task<bool> ExisteRegistro(Expression<Func<TEntity, bool>> predicate);
    }
}
