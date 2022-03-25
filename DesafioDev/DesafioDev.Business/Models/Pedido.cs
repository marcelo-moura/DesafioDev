using DesafioDev.Business.Enums;
using DesafioDev.Business.Models.Base;

namespace DesafioDev.Business.Models
{
    public class Pedido : Entity
    {
        public int Codigo { get; private set; }
        public Guid UsuarioId { get; set; }
        public decimal ValorTotal { get; set; }
        public EPedidoStatus PedidoStatus { get; set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public void AdicionarItem(PedidoItem item)
        {
            item.AssociarPedido(Id);

            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                itemExistente?.AdicionarUnidades(item.Quantidade);
                item = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            item?.CalcularValor();
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
        }

        public void SetStatusPedido(EPedidoStatus pedidoStatus) => PedidoStatus = pedidoStatus;
    }
}
