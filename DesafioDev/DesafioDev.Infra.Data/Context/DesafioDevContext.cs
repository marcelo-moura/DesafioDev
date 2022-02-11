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

        public DbSet<Produto> Produtos { get; set; }
    }
}
