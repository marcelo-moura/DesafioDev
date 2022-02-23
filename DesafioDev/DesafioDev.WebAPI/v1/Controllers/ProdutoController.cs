using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> Get()
        {
            return CustomResponse(await _produtoService.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _produtoService.FindById(id);
            if (produto == null) return NotFound();
            return CustomResponse(produto);
        }

        [HttpGet("getByCategoria/{categoriaId}")]
        public async Task<IActionResult> GetByCategoria(Guid? categoriaId)
        {
            var produtos = await _produtoService.FindByCategoria(categoriaId);
            if (produtos == null) return NotFound();
            return CustomResponse(produtos);
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
