using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Enums;
using DesafioDev.Business.Models;
using DesafioDev.Core.Extensions;
using DesafioDev.Core.Interfaces;
using DesafioDev.Core.Messages.IntegrationEvents;
using DesafioDev.Infra.Common.Utils;
using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.InterfacesRepository;
using NerdStore.Core.DomainObjects.DTO;

namespace DesafioDev.Application.Services
{
    public class PedidoService : ServiceBase<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository,
                             IPedidoItemRepository pedidoItemRepository,
                             IRabbitMQMessageSender rabbitMQMessageSender,
                             IMapper mapper,
                             INotificador notificador) : base(pedidoRepository, notificador)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoItemRepository = pedidoItemRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _mapper = mapper;
        }

        public async Task<PedidoViewModelSaida> AdicionarItemPedido(AdicionarItemPedidoViewModelEntrada pedidoEntrada)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorUsuarioId(pedidoEntrada.UsuarioId);
            var pedidoItem = new PedidoItem(pedidoEntrada.ProdutoId,
                                            pedidoEntrada.Nome,
                                            pedidoEntrada.Quantidade,
                                            pedidoEntrada.ValorUnitario);

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
                    await _pedidoItemRepository.Atualizar(
                        pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                else
                    await _pedidoItemRepository.Adicionar(pedidoItem);
            }

            return _mapper.Map<PedidoViewModelSaida>(pedido);
        }

        public async Task<PedidoViewModelSaida> IniciarPedido(CarrinhoViewModelEntrada carrinhoEntrada)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorUsuarioId(carrinhoEntrada.ClienteId);
            pedido.SetStatusPedido(EPedidoStatus.Iniciado);

            var itensList = new List<Item>();
            pedido.PedidoItems.ForEach(i => itensList.Add(new Item
            {
                Id = i.ProdutoId,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario
            }));

            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            var pedidoIniciadoEvent = new PedidoIniciadoEvent(carrinhoEntrada.PedidoId, carrinhoEntrada.ClienteId,
                                                              carrinhoEntrada.ClienteLogin,
                                                              carrinhoEntrada.ValorTotal, listaProdutosPedido,
                                                              carrinhoEntrada.Pagamento.NomeCartao,
                                                              carrinhoEntrada.Pagamento.NumeroCartao,
                                                              carrinhoEntrada.Pagamento.ExpiracaoCartao,
                                                              carrinhoEntrada.Pagamento.CvvCartao,
                                                              carrinhoEntrada.Pagamento.Parcelas,
                                                              carrinhoEntrada.Pagamento.TokenCard,
                                                              carrinhoEntrada.Pagamento.PaymentMethodId);

            _rabbitMQMessageSender.SendMessage(pedidoIniciadoEvent, "iniciar-pedido-queue");

            await _pedidoRepository.Atualizar(pedido);

            return _mapper.Map<PedidoViewModelSaida>(pedido);
        }

        public async Task<CarrinhoViewModelSaida> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorUsuarioId(clienteId);
            if (pedido == null) return null;

            var carrinho = new CarrinhoViewModelSaida
            {
                UsuarioId = pedido.UsuarioId,
                ValorTotal = pedido.ValorTotal,
                PedidoId = pedido.Id
            };

            foreach (var item in pedido.PedidoItems)
            {
                carrinho.Items.Add(new CarrinhoItemViewModelSaida
                {
                    ProdutoId = item.ProdutoId,
                    NomeProduto = item.NomeProduto,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorUnitario * item.Quantidade
                });
            }

            return carrinho;
        }
    }
}
