namespace DesafioDev.Business.Models.Custom
{
    public class ResultadoConsultaProduto
    {
        public Guid? CategoriaId { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string? NomeCategoria { get; set; }
        public string CodigoUsuarioCadastro { get; set; }
        public string NomeUsuarioCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public string? CodigoUsuarioAlteracao { get; set; }
        public string? NomeUsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
