using DesafioDev.Business.Models.Base;
using System.Linq.Expressions;

namespace DesafioDev.Application.Interfaces.Base
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> ObterPorId(Guid id);
        Task<IList<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        void Dispose();
    }
}
