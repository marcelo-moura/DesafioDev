using DesafioDev.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Data.Mappings
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Login);
            builder.Property(u => u.Senha);
            builder.Property(u => u.Codigo);
            builder.Property(u => u.NomeCompleto);
            builder.Property(u => u.RefreshToken);
            builder.Property(u => u.RefreshTokenExpiryTime);
            builder.Property(u => u.Ativo);

            builder.Property(p => p.CodigoUsuarioCadastro);
            builder.Property(p => p.NomeUsuarioCadastro);
            builder.Property(p => p.DataCadastro);
            builder.Property(p => p.CodigoUsuarioAlteracao);
            builder.Property(p => p.NomeUsuarioAlteracao);
            builder.Property(p => p.DataAlteracao);

            builder.ToTable("Usuario");
        }
    }
}
