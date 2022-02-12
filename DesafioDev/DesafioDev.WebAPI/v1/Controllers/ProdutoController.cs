using DesafioDev.Application.Interfaces;
using DesafioDev.Business.Models;
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
        public IActionResult Get()
        {
            return CustomResponse(_produtoService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var produto = _produtoService.FindById(id);
            if (produto == null) return NotFound();
            return CustomResponse(produto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            return CustomResponse(_produtoService.Create(produto));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Produto produto)
        {
            if (produto == null) return BadRequest();
            return CustomResponse((_produtoService.Update(produto)));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _produtoService.Delete(id);
            return CustomResponse();
        }
    }
}
