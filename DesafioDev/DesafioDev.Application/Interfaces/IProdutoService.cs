using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        Task<IList<ProdutoViewModelSaida>> FindAll();
        Task<ProdutoViewModelSaida> FindById(Guid id);
        Task<IEnumerable<ProdutoViewModelSaida>> FindByNome(string nome);
        Task<IList<ProdutoViewModelSaida>> FindByCategoria(Guid? categoriaId);
        Task<IEnumerable<ProdutoViewModelSaida>> FindByPreco(decimal? preco);
        Task<Produto> Create(ProdutoViewModelEntrada produtoEntrada);
        Task<Produto> Update(AtualizarProdutoViewModelEntrada atualizarProdutoEntrada);
        Task Delete(Guid id);
    }
}
