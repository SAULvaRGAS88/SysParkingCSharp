using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysParkingC_.Models
{
    public class Estacionamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome do Estacionamento")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [Display(Name = "Endereço")]
        [MaxLength(200, ErrorMessage = "O endereço não pode ter mais de 200 caracteres.")]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "O número de vagas disponíveis é obrigatório.")]
        [Display(Name = "Número de Vagas Disponíveis")]
        [Range(0, int.MaxValue, ErrorMessage = "O número de vagas deve ser maior ou igual a zero.")]
        public int NumeroVagasDisponiveis { get; set; }

        // Lista de usuários associados ao estacionamento
        [Display(Name = "Usuários")]
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        // Lista de notas fiscais associadas a este estacionamento
        [Display(Name = "Notas Fiscais")]
        public List<NotaFiscal> Notas { get; set; } = new List<NotaFiscal>();

        [Required(ErrorMessage = "O preço por 15 minutos é obrigatório.")]
        [Display(Name = "Preço por 15 Minutos")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public double Preco15Min { get; set; }

        [Required(ErrorMessage = "O preço por 30 minutos é obrigatório.")]
        [Display(Name = "Preço por 30 Minutos")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public double Preco30Min { get; set; }

        [Required(ErrorMessage = "O preço por 1 hora é obrigatório.")]
        [Display(Name = "Preço por 1 Hora")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public double Preco1Hora { get; set; }

        [Required(ErrorMessage = "O preço da diária é obrigatório.")]
        [Display(Name = "Preço da Diária")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public double PrecoDiaria { get; set; }

        [Required(ErrorMessage = "O preço do pernoite é obrigatório.")]
        [Display(Name = "Preço do Pernoite")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public double PrecoPernoite { get; set; }

        [Required(ErrorMessage = "O preço mensal é obrigatório.")]
        [Display(Name = "Preço Mensal")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public double PrecoMensal { get; set; }
    }
}
