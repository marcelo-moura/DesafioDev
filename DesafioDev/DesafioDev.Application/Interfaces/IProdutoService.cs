using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;
using DesafioDev.Core.Utils;

namespace DesafioDev.Application.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        Task<IList<ProdutoViewModelSaida>> FindAll();
        Task<ProdutoViewModelSaida> FindById(Guid id);
        Task<IEnumerable<ProdutoViewModelSaida>> FindByNome(string nome);
        Task<IList<ProdutoViewModelSaida>> FindByCategoria(Guid? categoriaId);
        Task<IEnumerable<ProdutoViewModelSaida>> FindByPreco(decimal? preco);
        Task<PagedSearchViewModel<ProdutoViewModelSaida>> FindPagedSearch(string name, string sortDirection, int pageSize, int page);
        Task<Produto> Create(ProdutoViewModelEntrada produtoEntrada);
        Task<Produto> Update(AtualizarProdutoViewModelEntrada atualizarProdutoEntrada);
        Task Delete(Guid id);
    }
}
