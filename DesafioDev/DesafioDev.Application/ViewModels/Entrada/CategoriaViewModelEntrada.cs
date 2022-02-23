using DesafioDev.Application.ViewModels.Base;

namespace DesafioDev.Application.ViewModels.Entrada
{
    public class CategoriaViewModelEntrada : EntradaViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        public List<ProdutoViewModelEntrada> Produtos { get; set; }
    }
}
