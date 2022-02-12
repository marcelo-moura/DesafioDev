using DesafioDev.Business.Models;

namespace DesafioDev.Application.Interfaces
{
    public interface IProdutoService
    {
        List<Produto> FindAll();
        Produto FindById(Guid id);
        Produto Create(Produto produto);
        Produto Update(Produto produto);
        void Delete(Guid id);
    }
}
