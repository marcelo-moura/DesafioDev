using DesafioDev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Data.Mappings
{
    public class PedidoConfig : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Codigo);
            builder.Property(p => p.UsuarioId);
            builder.Property(p => p.ValorTotal);
            builder.Property(p => p.PedidoStatus);

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.PedidoItems)
                .WithOne(c => c.Pedido)
                .HasForeignKey(c => c.PedidoId);

            builder.Property(p => p.CodigoUsuarioCadastro);
            builder.Property(p => p.NomeUsuarioCadastro);
            builder.Property(p => p.DataCadastro);

            builder.Ignore(p => p.Ativo);
            builder.Ignore(p => p.CodigoUsuarioAlteracao);
            builder.Ignore(p => p.NomeUsuarioAlteracao);
            builder.Ignore(p => p.DataAlteracao);

            builder.ToTable("Pedido");
        }
    }
}
