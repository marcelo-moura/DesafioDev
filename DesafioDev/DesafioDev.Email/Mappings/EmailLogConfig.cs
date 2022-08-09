using DesafioDev.Email.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Email.Mappings
{
    public class EmailLogConfig : IEntityTypeConfiguration<EmailLog>
    {
        public void Configure(EntityTypeBuilder<EmailLog> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email);
            builder.Property(e => e.Log);
            builder.Property(e => e.DataCadastro);

            builder.Ignore(e => e.CodigoUsuarioCadastro);
            builder.Ignore(e => e.NomeUsuarioCadastro);
            builder.Ignore(e => e.CodigoUsuarioAlteracao);
            builder.Ignore(e => e.NomeUsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

            builder.ToTable("EmailLog");
        }
    }
}
