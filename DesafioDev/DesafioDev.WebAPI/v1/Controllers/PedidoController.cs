using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PedidoController : BaseController
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(INotificador notificador,
                                IPedidoService pedidoService) : base(notificador)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdicionarItemPedidoViewModelEntrada pedidoEntrada)
        {
            if (pedidoEntrada == null) return BadRequest();
            return CustomResponse(await _pedidoService.IniciarPedido(pedidoEntrada));
        }
    }
}
