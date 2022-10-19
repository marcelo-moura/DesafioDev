using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Core.Hypermedia.Filters;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(INotificador notificador,
                                 IProdutoService produtoService) : base(notificador)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Get()
        {
            return CustomResponse(await _produtoService.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _produtoService.FindById(id);
            if (produto == null) return NotFound();
            return CustomResponse(produto);
        }

        [HttpGet("getByNome/{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            var produtos = await _produtoService.FindByNome(nome);
            if (produtos == null) return NotFound();
            return CustomResponse(produtos);
        }

        [HttpGet("getByCategoria/{categoriaId}")]
        public async Task<IActionResult> GetByCategoria(Guid? categoriaId)
        {
            var produtos = await _produtoService.FindByCategoria(categoriaId);
            if (produtos == null) return NotFound();
            return CustomResponse(produtos);
        }

        [HttpGet("getByPreco/{preco}")]
        public async Task<IActionResult> GetByPreco(decimal? preco)
        {
            var produtos = await _produtoService.FindByPreco(preco);
            if (produtos == null) return NotFound();
            return CustomResponse(produtos);
        }

        [HttpGet("{page}/{pageSize}/{sortOrder}/{sortDirection}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetPagedSearch([FromQuery] FiltroProdutoViewModelEntrada filtroProduto, int page = 1, int pageSize = 20, int sortOrder = 1, string sortDirection = "asc")
        {
            var produtos = await _produtoService.FindPagedSearch(filtroProduto, page, pageSize, sortOrder, sortDirection);
            return CustomResponse(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoViewModelEntrada produtoEntrada)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);
            return CustomResponse(await _produtoService.Create(produtoEntrada));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AtualizarProdutoViewModelEntrada atualizarProdutoEntrada)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);
            return CustomResponse(await _produtoService.Update(atualizarProdutoEntrada));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _produtoService.Delete(id);
            return CustomResponse();
        }
    }
}
