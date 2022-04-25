using AutoMapper;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;
using DesafioDev.Core.Interfaces;
using DesafioDev.Infra.Integration.Interfaces;
using DesafioDev.Infra.InterfacesRepository;
using Microsoft.Extensions.Configuration;

namespace DesafioDev.Application.Services
{
    public class PagamentoService : ServiceBase<Pagamento>, IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PagamentoService(IPagamentoRepository pagamentoRepository,
                                IUsuarioRepository usuarioRepository,
                                IPedidoRepository pedidoRepository,
                                IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                                IMapper mapper,
                                IConfiguration configuration,
                                INotificador notificador) : base(pagamentoRepository, notificador)
        {
            _pagamentoRepository = pagamentoRepository;
            _usuarioRepository = usuarioRepository;
            _pedidoRepository = pedidoRepository;
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PagamentoViewModelSaida> RealizarPagamentoPedido(PagamentoViewModelEntrada pagamentoEntrada)
        {
            var usuario = await _usuarioRepository.BuscarPor(u => u.Id == pagamentoEntrada.UsuarioId);
            var pedido = await _pedidoRepository.ObterPedidoComItensPorId(pagamentoEntrada.PedidoId);

            pagamentoEntrada.TokenCard = _configuration["MercadoPagoAPI:TokenCardTest"];
            var pagamentoRequest = _mapper.Map<Pagamento>(pagamentoEntrada);

            var pagamento = _pagamentoCartaoCreditoFacade.RealizarPagamento(usuario, pedido, pagamentoRequest);

            return new PagamentoViewModelSaida();
        }
    }
}
