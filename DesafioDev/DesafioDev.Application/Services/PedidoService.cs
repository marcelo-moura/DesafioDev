using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Enums;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Infra.Common.Utils;
using DesafioDev.Infra.InterfacesRepository;

namespace DesafioDev.Application.Services
{
    public class PedidoService : ServiceBase<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository,
                             IPedidoItemRepository pedidoItemRepository,
                             IMapper mapper,
                             INotificador notificador) : base(pedidoRepository, notificador)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoItemRepository = pedidoItemRepository;
            _mapper = mapper;
        }

        public async Task<PedidoViewModelSaida> AdicionarItemPedido(AdicionarItemPedidoViewModelEntrada pedidoEntrada)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorUsuarioId(pedidoEntrada.UsuarioId);
            var pedidoItem = new PedidoItem(pedidoEntrada.ProdutoId, pedidoEntrada.Nome, pedidoEntrada.Quantidade, pedidoEntrada.ValorUnitario);

            if (pedido == null)
            {
                pedido = _mapper.Map<Pedido>(pedidoEntrada);
                pedido.AdicionarItem(pedidoItem);
                pedido.SetCodigoPedido(Utils.GerarCodigoPedido());
                pedido.SetStatusPedido(EPedidoStatus.Rascunho);

                await _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                var pedidoItemExiste = pedido.PedidoItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExiste)
                    await _pedidoItemRepository.Atualizar(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                else
                    await _pedidoItemRepository.Adicionar(pedidoItem);
            }

            return _mapper.Map<PedidoViewModelSaida>(pedido);
        }
    }
}
