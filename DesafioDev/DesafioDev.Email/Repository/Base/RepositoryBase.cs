using DesafioDev.Email.Context;
using DesafioDev.Email.InterfacesRepository.Base;
using DesafioDev.Email.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Email.Repository.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContextOptions<DesafioDevEmailContext> Db;
        protected readonly DesafioDevEmailContext DbSet;

        public RepositoryBase(DbContextOptions<DesafioDevEmailContext> context)
        {
            Db = context;
            DbSet = new DesafioDevEmailContext(context);
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
