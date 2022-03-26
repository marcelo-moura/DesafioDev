using DesafioDev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Data.Mappings
{
    public class PedidoItemConfig : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.PedidoId);
            builder.Property(p => p.ProdutoId);
            builder.Property(p => p.NomeProduto);
            builder.Property(p => p.Quantidade);
            builder.Property(p => p.ValorUnitario);

            builder.HasOne(c => c.Pedido)
                .WithMany(c => c.PedidoItems);

            builder.Ignore(p => p.Ativo);

            builder.ToTable("PedidoItem");
        }
    }
}
