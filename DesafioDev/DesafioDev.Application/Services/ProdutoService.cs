using DesafioDev.Application.Interfaces;
using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;

namespace DesafioDev.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly DesafioDevContext _context;
        public ProdutoService(DesafioDevContext context)
        {
            _context = context;
        }

        public List<Produto> FindAll()
        {
            return _context.Produtos.ToList();
        }

        public Produto FindById(Guid id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public Produto Create(Produto produto)
        {
            try
            {
                _context.Add(produto);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return produto;
        }
        public Produto Update(Produto produto)
        {
            if (!Exists(produto.Id)) return null;

            var result = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id);

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(produto);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return produto;
        }

        public void Delete(Guid id)
        {
            var result = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Produtos.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private bool Exists(Guid id)
        {
            return _context.Produtos.Any(p => p.Id == id);
        }
    }
}
