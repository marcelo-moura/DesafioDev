using DesafioDev.Application.ViewModels.Base;
using DesafioDev.Business.Enums;

namespace DesafioDev.Application.ViewModels.Entrada
{
    public class AdicionarItemPedidoViewModelEntrada : EntradaViewModel
    {
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
