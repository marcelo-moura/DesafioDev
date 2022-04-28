﻿using DesafioDev.Business.Models.Base;

namespace DesafioDev.Business.Models
{
    public class Pagamento : Entity
    {
        public Guid PedidoId { get; private set; }
        public decimal Valor { get; private set; }
        public int Parcelas { get; private set; }
        public string PaymentMethodId { get; private set; }
        public string TokenCard { get; private set; }
    }
}
