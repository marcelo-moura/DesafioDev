using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.Interfaces
{
    public interface IProdutoService : IServiceBase<Produto>
    {
        Task<IList<Produto>> FindAll();
        Task<Produto> FindById(Guid id);
        Task<Produto> Create(Produto produto);
        Task<Produto> Update(Produto produto);
        Task Delete(Guid id);
    }
}
