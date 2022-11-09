using DesafioDev.Application.Interfaces;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Core.Interfaces;
using DesafioDev.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDev.WebAPI.v1.Controllers
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarrinhoController : BaseController
    {
        private readonly IPedidoService _pedidoService;
        private readonly IProdutoService _produtoService;

        public CarrinhoController(INotificador notificador,
                                  IPedidoService pedidoService,
                                  IProdutoService produtoService) : base(notificador)
        {
            _pedidoService = pedidoService;
            _produtoService = produtoService;
        }

        [HttpGet]
        [Route("meu-carrinho")]
        public async Task<IActionResult> Index(Guid clienteId)
        {
            return CustomResponse(await _pedidoService.ObterCarrinhoCliente(clienteId));
        }

        [HttpPost]
        [Route("adicionar-item")]
        public async Task<IActionResult> AdicionarItem([FromBody] AdicionarItemPedidoViewModelEntrada pedidoEntrada)
        {
            if (pedidoEntrada == null) return BadRequest();

            var produto = await _produtoService.ObterPorId(pedidoEntrada.ProdutoId);
            if (produto == null) return BadRequest();

            if (produto.Quantidade < pedidoEntrada.Quantidade) return BadRequest("Produto com estoque insuficiente.");

            return CustomResponse(await _pedidoService.AdicionarItemPedido(pedidoEntrada));
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoViewModelEntrada carrinhoEntrada)
        {
            if (carrinhoEntrada?.ClienteId == null) return BadRequest();

            var carrinho = await _pedidoService.ObterCarrinhoCliente(carrinhoEntrada.ClienteId);
            if (carrinho == null) return BadRequest();

            return CustomResponse(await _pedidoService.IniciarPedido(carrinhoEntrada));
        }
    }
}
