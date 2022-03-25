using DesafioDev.Application.Interfaces;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Post([FromBody] Pedido produtoEntrada)
        {
            if (produtoEntrada == null) return BadRequest();
            return CustomResponse(await _pedidoService.IniciarPedido(produtoEntrada));
        }
    }
}
