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

        public override Task<IList<Produto>> BuscarComPagedSearch(string nameProcedure, string pesquisa, string sort, int offset, int size)
        {
            return base.BuscarComPagedSearch(nameProcedure, pesquisa, sort, offset, size);
        }

        public async Task<int> GetCountName(string pesquisa)
        {
            return await Db.Produtos.AsNoTracking().Where(p => p.Nome.Contains(pesquisa)).CountAsync();
        }

        public async Task<List<Produto>> ObterPorCategoria(Guid? categoriaId)
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.Categoria).Where(p => p.Categoria.Id == categoriaId).ToListAsync();
        }
    }
}
