using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Business.Enums;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Application.Services
{
    public class PedidoService : ServiceBase<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository,
                             IMapper mapper,
                             INotificador notificador) : base(pedidoRepository, notificador)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<Pedido> IniciarPedido(AdicionarItemPedidoViewModelEntrada pedidoEntrada)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorUsuarioId(pedidoEntrada.UsuarioId);
            var pedidoItem = new PedidoItem(pedidoEntrada.ProdutoId, pedidoEntrada.Nome, pedidoEntrada.Quantidade, pedidoEntrada.ValorUnitario);

            if (pedido == null)
            {
                pedido = _mapper.Map<Pedido>(pedidoEntrada);
                pedido.AdicionarItem(pedidoItem);
                pedido.SetStatusPedido(EPedidoStatus.Rascunho);

                await _pedidoRepository.Adicionar(pedido);
            }

            return pedido;
        }
    }
}
