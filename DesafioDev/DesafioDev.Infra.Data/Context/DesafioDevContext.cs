using DesafioDev.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Infra.Data.Context
{
    public class DesafioDevContext : DbContext
    {
        public DesafioDevContext()
        { }

        public DesafioDevContext(DbContextOptions<DesafioDevContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                     .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioDevContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
