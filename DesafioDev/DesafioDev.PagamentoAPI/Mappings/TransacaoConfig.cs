using DesafioDev.PagamentoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.PagamentoAPI.Mappings
{
    public class TransacaoConfig : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.PedidoId);
            builder.Property(t => t.PagamentoId);
            builder.Property(t => t.Total);
            builder.Property(t => t.StatusTransacao);

            builder.Ignore(x => x.Ativo);

            builder.ToTable("Transacao");
        }
    }
}