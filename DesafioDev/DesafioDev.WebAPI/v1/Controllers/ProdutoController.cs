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
            return Ok(await _produtoService.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _produtoService.FindById(id);
            if (produto == null) return NotFound();
            return Ok(produto);
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

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetPagedSearch([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            var produtos = await _produtoService.FindPagedSearch(name, sortDirection, pageSize, page);
            if (produtos == null) return NotFound();
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoViewModelEntrada produtoEntrada)
        {
            if (produtoEntrada == null) return BadRequest();
            return CustomResponse(await _produtoService.Create(produtoEntrada));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AtualizarProdutoViewModelEntrada atualizarProdutoEntrada)
        {
            if (atualizarProdutoEntrada == null) return BadRequest();
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
