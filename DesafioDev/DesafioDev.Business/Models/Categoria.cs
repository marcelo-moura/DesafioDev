using DesafioDev.Business.Models.Base;

namespace DesafioDev.Business.Models
{
    public class Categoria : Entity
    {
        public int Codigo { get; private set; }
        public string Nome { get; private set; }

        public ICollection<Produto> Produtos { get; private set; }

        protected Categoria() { }

        public Categoria(int codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }
    }
}
