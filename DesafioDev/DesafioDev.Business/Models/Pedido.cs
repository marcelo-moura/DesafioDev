using DesafioDev.Business.Enums;
using DesafioDev.Business.Models.Base;
using DesafioDev.Core.DomainObjects;
using DesafioDev.Infra.Common.Utils;

namespace DesafioDev.Business.Models
{
    public class Pedido : Entity
    {
        public string Codigo { get; private set; }
        public Guid UsuarioId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public EPedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public void SetCodigoPedido(string codigoPedido) => Codigo = codigoPedido;
        public void SetStatusPedido(EPedidoStatus pedidoStatus) => PedidoStatus = pedidoStatus;

        public void AdicionarItem(PedidoItem item)
        {
            ValidarQuantidadeItemPermitida(item);

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

        private void ValidarQuantidadeItemPermitida(PedidoItem item)
        {
            var quantidadeItems = item.Quantidade;
            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                quantidadeItems += itemExistente.Quantidade;
            }

            if (quantidadeItems > Constantes.MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de {Constantes.MAX_UNIDADES_ITEM} unidades por produto.");
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid usuarioId)
            {
                var pedido = new Pedido
                {
                    UsuarioId = usuarioId,
                };

                pedido.SetStatusPedido(EPedidoStatus.Rascunho);
                return pedido;
            }
        }
    }
}
