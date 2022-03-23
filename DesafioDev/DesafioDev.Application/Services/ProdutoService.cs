using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Utils;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Application.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoService(IProdutoRepository produtoRepository,
                              IMapper mapper,
                              INotificador notificador) : base(produtoRepository, notificador)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IList<ProdutoViewModelSaida>> FindAll()
        {
            return _mapper.Map<IList<ProdutoViewModelSaida>>(await _produtoRepository.ObterTodos());
        }

        public async Task<ProdutoViewModelSaida> FindById(Guid id)
        {
            return _mapper.Map<ProdutoViewModelSaida>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<ProdutoViewModelSaida>> FindByNome(string nome)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModelSaida>>(await _produtoRepository.Buscar(p => p.Nome.Contains(nome)));
        }

        public async Task<IList<ProdutoViewModelSaida>> FindByCategoria(Guid? categoriaId)
        {
            return _mapper.Map<IList<ProdutoViewModelSaida>>(await _produtoRepository.ObterPorCategoria(categoriaId));
        }

        public async Task<IEnumerable<ProdutoViewModelSaida>> FindByPreco(decimal? preco)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModelSaida>>(await _produtoRepository.Buscar(p => p.Preco == preco));
        }

        public async Task<PagedSearchViewModel<ProdutoViewModelSaida>> FindPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            var produtos = await _produtoRepository.BuscarComPagedSearch("sp_ListarProdutos", name, sort, offset, size);
            int totalResults = await _produtoRepository.GetCountName(name);

            return new PagedSearchViewModel<ProdutoViewModelSaida>
            {
                CurrentPage = page,
                ListObject = _mapper.Map<List<ProdutoViewModelSaida>>(produtos),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults,
            };
        }

        public async Task<Produto> Create(ProdutoViewModelEntrada produtoEntrada)
        {
            var produto = _mapper.Map<Produto>(produtoEntrada);

            if (produto.CategoriaId == Guid.Empty) produto.SetCategoriaId(null);

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
        public async Task<Produto> Update(AtualizarProdutoViewModelEntrada atualizarProdutoEntrada)
        {
            if (!await _produtoRepository.ExisteRegistro(p => p.Id == atualizarProdutoEntrada.Id)) return null;

            var result = await _produtoRepository.BuscarPor(p => p.Id == atualizarProdutoEntrada.Id);
            var produto = PreencherDadosAtualizarProduto(atualizarProdutoEntrada, result);

            if (result != null)
            {
                try
                {
                    await _produtoRepository.Atualizar(_mapper.Map<Produto>(produto));
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

        private Produto PreencherDadosAtualizarProduto(AtualizarProdutoViewModelEntrada atualizarProdutoEntrada, Produto produtoExistente)
        {
            var produto = _mapper.Map<Produto>(atualizarProdutoEntrada);

            if (produto.CategoriaId == Guid.Empty) produto.SetCategoriaId(null);

            produto.DataAlteracao = DateTime.Now;
            produto.CodigoUsuarioCadastro = produtoExistente.CodigoUsuarioCadastro;
            produto.NomeUsuarioCadastro = produtoExistente.NomeUsuarioCadastro;
            produto.DataCadastro = produtoExistente.DataCadastro;

            return produto;
        }
    }
}
