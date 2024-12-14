using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysParkingC_.Models
{
    public class NotaFiscal
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CarroId")]
        [Display(Name = "Carro")]
        public int? CarroId { get; set; }

        public Carro? Carro { get; set; }

        [Required(ErrorMessage = "A Data de Saída é obrigatória.")]
        [Display(Name = "Data de Saída")]
        [DataType(DataType.Date)]
        public DateTime DataSaida { get; set; }

        [Required(ErrorMessage = "A Hora de Saída é obrigatória.")]
        [Display(Name = "Hora de Saída")]
        [DataType(DataType.Time)]
        public DateTime HoraSaida { get; set; }

        [Required(ErrorMessage = "O tipo de pagamento é obrigatório.")]
        [Display(Name = "Tipo de Pagamento")]
        public TipoPagamento Pagamento { get; set; }

        [Display(Name = "Tempo de Permanência")]
        [MaxLength(50, ErrorMessage = "O tempo de permanência não pode exceder 50 caracteres.")]
        public string? TempoPermanencia { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O valor total deve ser maior ou igual a zero.")]
        public double? ValorTotal { get; set; }
    }

    public enum TipoPagamento
    {
        [Description("Selecione uma opção")]
        DEFAULT = -1,

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
