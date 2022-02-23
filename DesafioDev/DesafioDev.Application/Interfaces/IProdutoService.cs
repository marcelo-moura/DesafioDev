using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        Task<IList<Produto>> FindAll();
        Task<Produto> FindById(Guid id);
        Task<IList<ProdutoViewModelSaida>> FindByCategoria(Guid? categoriaId);
        Task<Produto> Create(ProdutoViewModelEntrada produtoEntrada);
        Task<Produto> Update(AtualizarProdutoViewModelEntrada atualizarProdutoEntrada);
        Task Delete(Guid id);
    }
}
