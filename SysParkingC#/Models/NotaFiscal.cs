using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysParkingC_.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }

        public int CarroId { get; set; }

        [ForeignKey("CarroId")]
        public Carro? Carro { get; set; }

        public DateTime DataSaida { get; set; }
        public DateTime HoraSaida { get; set; }
        public TipoPagamento Pagamento { get; set; }
    }

    public enum TipoPagamento
    {
        [Description("Dinheiro")]
        DINHEIRO = 0,

        [Description("Cartão de Crédito")]
        CARTAO_CREDITO = 1,

        [Description("Cartão de Débito")]
        CARTAO_DEBITO = 2,

        [Description("Pix")]
        PIX = 3
    }
}
