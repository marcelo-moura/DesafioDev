namespace DesafioDev.Email.Models.Base
{
    public abstract class Entity : EntityBase
    {
        public string CodigoUsuarioCadastro { get; set; }
        public string NomeUsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public string? CodigoUsuarioAlteracao { get; set; }
        public string? NomeUsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
