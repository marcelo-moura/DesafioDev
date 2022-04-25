using DesafioDev.Application.Interfaces.Base;
using DesafioDev.Application.ViewModels.Entrada;
using DesafioDev.Application.ViewModels.Saida;
using DesafioDev.Business.Models;

namespace DesafioDev.Application.Interfaces
{
    public interface IPagamentoService : IServiceBase<Pagamento>
    {
        Task<PagamentoViewModelSaida> RealizarPagamentoPedido(PagamentoViewModelEntrada pagamentoEntrada);
    }
}
