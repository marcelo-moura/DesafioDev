using DesafioDev.PagamentoAPI.Context;
using DesafioDev.PagamentoAPI.InterfacesRepository.Base;
using DesafioDev.PagamentoAPI.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.PagamentoAPI.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContextOptions<DesafioDevPagamentoContext> Db;
        protected readonly DesafioDevPagamentoContext DbSet;

        public RepositoryBase(DbContextOptions<DesafioDevPagamentoContext> context)
        {
            Db = context;
            DbSet = new DesafioDevPagamentoContext(context);
        }

        public virtual async Task<bool> Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            return await SaveChanges() > 0;
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        private Task<int> SaveChanges()
        {
            return DbSet.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbSet?.Dispose();
        }
    }
}
