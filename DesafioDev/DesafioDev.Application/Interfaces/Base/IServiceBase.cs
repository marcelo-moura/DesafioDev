using DesafioDev.Business.Models.Base;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Utils;
using System.Linq.Expressions;

namespace DesafioDev.Application.Interfaces.Base
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> ObterPorId(Guid id);
        Task<IList<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<PagedSearchViewModel<TSaida>> FindPagedSearch<TSaida, TFiltroEntrada, TEntityCustom>
            (TFiltroEntrada filtroEntrada, int page, int pageSize, int sortOrder, string sortDirection, string nameProcedure = "")
            where TSaida : ISupportsHyperMedia
            where TEntityCustom : class;
        void Dispose();
    }
}
