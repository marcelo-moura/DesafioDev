using DesafioDev.Application.ViewModels.Base;
using DesafioDev.Core.Hypermedia;
using DesafioDev.Core.Interfaces;

namespace DesafioDev.Application.ViewModels.Saida
{
    public class ProdutoViewModelSaida : EntradaViewModel, ISupportsHyperMedia
    {
        public Guid? CategoriaId { get; set; }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
