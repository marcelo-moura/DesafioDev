using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Business.Models.Base;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Notificacoes;
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
