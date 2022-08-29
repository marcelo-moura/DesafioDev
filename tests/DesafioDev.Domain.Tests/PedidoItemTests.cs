using DesafioDev.Business.Models;
using DesafioDev.Core.DomainObjects;
using DesafioDev.Infra.Common.Utils;
using Xunit;

namespace DesafioDev.Domain.Tests
{
    public class PedidoItemTests
    {
        [Fact(DisplayName = "Novo Item Pedido Unidade Abaxio do Permitido")]
        [Trait("Categoria", "Pedido Item - Adicionar Item")]
        public void AdicionarItemPedido_UnidadesItemAbaixoDoPermitido_DeveRetornarException()
        {
            //Arrange, Act e Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(),
                                                                "Produto Teste",
                                                                Constantes.MIN_UNIDADES_ITEM - 1,
                                                                100));
        }
    }
}
