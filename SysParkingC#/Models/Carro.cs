using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysParkingC_.Models
{
    public class Carro
    {
        // Construtor padrão necessário para model binding
        public Carro() { }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50, ErrorMessage = "A marca pode ter no máximo 50 caracteres.")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O modelo pode ter no máximo 50 caracteres.")]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [StringLength(30, ErrorMessage = "A cor pode ter no máximo 30 caracteres.")]
        public string? Cor { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(@"^[A-Z0-9]{7}$", ErrorMessage = "A placa deve conter exatamente 7 caracteres alfanuméricos.")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve conter exatamente 7 caracteres.")]
        public string? Placa { get; set; }

        [Required(ErrorMessage = "A data de entrada é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Entrada")]
        public DateTime DataEntrada { get; set; }

        [Required(ErrorMessage = "A hora de entrada é obrigatória.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Entrada")]
        public DateTime HoraEntrada { get; set; }

        // Relacionamento com NotaFiscal (um para um)
        [Display(Name = "Nota Fiscal")]
        public NotaFiscal? NotaFiscal { get; set; }

        // Relacionamento com Estacionamento (muitos para um)
        [Required]
        [ForeignKey("Estacionamento")]
        [Display(Name = "Estacionamento")]
        public int EstacionamentoId { get; set; }

        public Estacionamento? Estacionamento { get; set; }
    }
}
