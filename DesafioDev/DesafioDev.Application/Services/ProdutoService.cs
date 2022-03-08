using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
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
