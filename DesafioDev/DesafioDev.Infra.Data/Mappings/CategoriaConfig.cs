using DesafioDev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Data.Mappings
{
    public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo);
            builder.Property(c => c.Nome);

            builder.Property(c => c.CodigoUsuarioCadastro);
            builder.Property(c => c.NomeUsuarioCadastro);
            builder.Property(c => c.DataCadastro);
            builder.Property(c => c.CodigoUsuarioAlteracao);
            builder.Property(c => c.NomeUsuarioAlteracao);
            builder.Property(c => c.DataAlteracao);

            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Categoria");
        }
    }
}
