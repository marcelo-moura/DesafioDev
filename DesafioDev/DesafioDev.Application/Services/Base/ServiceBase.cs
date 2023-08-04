using AutoMapper;
using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Application.ViewModels.Base;
using DesafioDev.Business.Models.Base;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Notificacoes;
using DesafioDev.Core.Utils;
using DesafioDev.Infra.Common.Utils;
using DesafioDev.Infra.Globalization;
using DesafioDev.Infra.InterfacesRepository.Base;
using System.Linq.Expressions;

namespace DesafioDev.Application.Services.Base
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        private IRepositoryBase<TEntity> _repositoryBase;
        private readonly INotificador _notificador;

        protected ServiceBase(IRepositoryBase<TEntity> repositoryBase,
                              INotificador notificador)
        {
            _repositoryBase = repositoryBase;
            _notificador = notificador;
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await _repositoryBase.ObterPorId(id);
        }

        public async Task<IList<TEntity>> ObterTodos()
        {
            return await _repositoryBase.ObterTodos();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repositoryBase.Buscar(predicate);
        }

        public async Task<PagedSearchViewModel<TSaida>> FindPagedSearch<TSaida, TFiltroEntrada, TEntityCustom>(TFiltroEntrada filtro, int page, int pageSize, int sortOrder, string sortDirection, string nameProcedure)
            where TSaida : ISupportsHyperMedia
            where TEntityCustom : class
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(BaseViewModel).Assembly));
            var _mapper = new Mapper(configuration);

            var listaPaginada = await _repositoryBase.BuscarComPagedSearch<TEntityCustom>(nameProcedure, sortOrder, sortDirection);

            var listObject = _mapper.Map<List<TSaida>>(listaPaginada);

            var predicate = Utils.MontarPredicateFiltro<TSaida, TFiltroEntrada>(filtro);

            if (predicate.IsStarted)
                listObject = listObject.Where(predicate).ToList();

            if (!listObject.Any())
            {
                Notificar(TextoGeral.NenhumRegistroEncontrado);
                return null;
            }

            return new PagedSearchViewModel<TSaida>
            {
                CurrentPage = page,
                ListObject = listObject.Skip(QuantidadeRegistrosParaDesconsiderar(page, pageSize)).Take(pageSize).ToList(),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = listObject.Count
            };
        }

        public void Dispose()
        {
            _repositoryBase?.Dispose();
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected int QuantidadeRegistrosParaDesconsiderar(int pagina, int registrosPorPagina)
        {
            return (pagina - 1) * registrosPorPagina;
        }
    }
}
