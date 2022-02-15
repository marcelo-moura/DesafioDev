using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Application.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository,
                              INotificador notificador) : base(produtoRepository, notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IList<Produto>> FindAll()
        {
            return await _produtoRepository.ObterTodos();
        }

        public async Task<Produto> FindById(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }

        public async Task<Produto> Create(Produto produto)
        {
            try
            {
                await _produtoRepository.Adicionar(produto);
            }
            catch (Exception)
            {
                throw;
            }
            return produto;
        }
        public async Task<Produto> Update(Produto produto)
        {
            if (!await _produtoRepository.ExisteRegistro(p => p.Id == produto.Id)) return null;

            var result = await _produtoRepository.BuscarPor(p => p.Id == produto.Id);

            if (result != null)
            {
                try
                {
                    await _produtoRepository.Atualizar(produto);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return produto;
        }

        public async Task Delete(Guid id)
        {
            try
            {
                await _produtoRepository.Remover(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
