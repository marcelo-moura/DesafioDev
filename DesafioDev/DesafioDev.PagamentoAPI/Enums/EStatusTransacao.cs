using System.ComponentModel;

namespace DesafioDev.PagamentoAPI.Enums
{
    public enum EStatusTransacao
    {
        [Description("Pago")]
        Pago = 1,
        [Description("Recusado")]
        Recusado = 2
    }
}
