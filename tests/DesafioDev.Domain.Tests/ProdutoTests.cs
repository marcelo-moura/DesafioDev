using DesafioDev.Business.Models;
using DesafioDev.Core.DomainObjects;
using Xunit;

namespace DesafioDev.Domain.Tests
{
    public class ProdutoTests
    {
        [Fact(DisplayName = "Debitar Estoque Produto com Quantidade Menor do que a Informada")]
        [Trait("Categoria", "Produto - Debitar Estoque")]
        public void DebitarEstoque_QuantidadeProdutoNoEstoqueMenorQueQuantidadeQueSeraDebitada_DeveRetornarException()
        {
            //Arrange
            var produto = new Produto(Guid.NewGuid(), "Produto Teste", "Descricao Teste", true, 100, 5);
            var quantidadeSeraDebitada = 7;

            //Act e Assert
            Assert.Throws<DomainException>(() => produto.DebitarEstoque(quantidadeSeraDebitada));
        }
    }
}
