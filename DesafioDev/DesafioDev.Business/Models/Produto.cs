using DesafioDev.Business.Models.Base;
using DesafioDev.Core.DomainObjects;

namespace DesafioDev.Business.Models
{
    public class Produto : Entity
    {
        public Guid? CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public Categoria Categoria { get; private set; }

        protected Produto() { }

        public Produto(Guid categoriaId, string nome, string descricao, bool ativo, decimal preco, int quantidade)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Preco = preco;
            Quantidade = quantidade;
        }

        public void ReporEstoque(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            if (!PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente!");

            Quantidade -= quantidade;
        }

        private bool PossuiEstoque(int quantidade)
        {
            return Quantidade >= quantidade;
        }
    }
}
