using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PagamentoController : BaseController
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoController(INotificador notificador,
                                   IPagamentoService pagamentoService) : base(notificador)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost]
        public async Task<PagamentoViewModelSaida> RealizarPagamento()
        {
            return await _pagamentoService.RealizarPagamentoPedido();
        }
    }
}
