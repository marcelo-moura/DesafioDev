using DesafioDev.Business.Models.Base;
using DesafioDev.Core.DomainObjects;
using DesafioDev.Infra.Common.Utils;

namespace DesafioDev.Business.Models
{
    public class PedidoItem : EntityBase
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public Pedido Pedido { get; private set; }
        public Produto Produto { get; private set; }

        protected PedidoItem() { }

        public PedidoItem(Guid produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Constantes.MIN_UNIDADES_ITEM) throw new DomainException($"Mínimo de {Constantes.MIN_UNIDADES_ITEM} unidades por produto");

            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        internal void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }
    }
}