using DesafioDev.Business.Models;
using DesafioDev.Infra.Data.Context;
using DesafioDev.Infra.Data.Repository.Base;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.EntityFrameworkCore;

namespace DesafioDev.Infra.Data.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DesafioDevContext context) : base(context)
        {
        }

        public async Task<List<Produto>> ObterPorCategoria(Guid? categoriaId)
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.Categoria).Where(p => p.Categoria.Id == categoriaId).ToListAsync();
        }
    }
}
