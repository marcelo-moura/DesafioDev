using DesafioDev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Data.Mappings
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Nome);
            builder.Property(p => p.Descricao);
            builder.Property(p => p.Ativo);
            builder.Property(p => p.Preco);
            builder.Property(p => p.Quantidade);

            builder.Property(p => p.CodigoUsuarioCadastro);
            builder.Property(p => p.NomeUsuarioCadastro);
            builder.Property(p => p.DataCadastro);
            builder.Property(p => p.CodigoUsuarioAlteracao);
            builder.Property(p => p.NomeUsuarioAlteracao);
            builder.Property(p => p.DataAlteracao);

            builder.ToTable("Produto");
        }
    }
}
