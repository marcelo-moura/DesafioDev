using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Core.Hypermedia.Filters;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VitrineController : BaseController
    {
        private readonly IProdutoService _produtoService;
        public VitrineController(INotificador notificador,
                                 IProdutoService produtoService) : base(notificador)
        {
            _produtoService = produtoService;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Get(Guid id)
        {
            var produto = await _produtoService.FindById(id);
            if (produto == null) return NotFound();
            return CustomResponse(produto);
        }

        [HttpGet("{page}/{pageSize}/{sortOrder}/{sortDirection}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetPagedSearch([FromQuery] FiltroProdutoViewModelEntrada filtroProduto, int page = 1, int pageSize = 20, int sortOrder = 1, string sortDirection = "asc")
        {
            var produtos = await _produtoService.FindPagedSearch<VitrineProdutoViewModelSaida, FiltroProdutoViewModelEntrada>(filtroProduto, page, pageSize, sortOrder, sortDirection);
            return CustomResponse(produtos);
        }                
    }
}
