using DesafioDev.Application.ViewModels.Base;

namespace DesafioDev.Application.ViewModels.Saida
{
    public class PedidoViewModelSaida : EntradaViewModel
    {
        public string Codigo { get; set; }
        public Guid UsuarioId { get; set; }
        public decimal ValorTotal { get; set; }
        public int PedidoStatus { get; set; }
        public List<PedidoItemViewModelSaida> PedidoItemsSaida { get; set; }
    }
}
