﻿namespace DesafioDev.Application.ViewModels.Entrada
{
    public class CarrinhoViewModelEntrada
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public string VoucherCodigo { get; set; }

        public List<CarrinhoItemViewModelEntrada> Items { get; set; } = new List<CarrinhoItemViewModelEntrada>();
        public CarrinhoPagamentoViewModelEntrada Pagamento { get; set; }
    }
}