using DesafioDev.Application.ViewModels.Base;

namespace DesafioDev.Application.ViewModels.Entrada
{
    public class ProdutoViewModelEntrada : EntradaViewModel
    {
        public Guid? CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}
