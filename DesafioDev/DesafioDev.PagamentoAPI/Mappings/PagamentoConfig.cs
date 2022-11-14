using DesafioDev.PagamentoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.PagamentoAPI.Mappings
{
    public class PagamentoConfig : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.PedidoId);
            builder.Property(c => c.NomeCartao);
            builder.Property(c => c.NumeroCartao);
            builder.Property(c => c.ExpiracaoCartao);
            builder.Property(c => c.CvvCartao);

            // 1 : 1 => Pagamento : Transacao
            builder.HasOne(p => p.Transacao)
                .WithOne(p => p.Pagamento);

            builder.Ignore(x => x.Ativo);
            builder.Ignore(x => x.PaymentMethodId);
            builder.Ignore(x => x.TokenCard);

            builder.ToTable("Pagamento");
        }
    }
}
