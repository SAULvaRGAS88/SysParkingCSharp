using Microsoft.EntityFrameworkCore;
using SysParkingC_.Data;
using System.ComponentModel.DataAnnotations;

namespace SysParkingC_.Models
{
    public class Carro
    {

        // Construtor padrão necessário para model binding
        public Carro() { }

        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Cor { get; set; }
        public string? Placa { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataEntrada { get; set; }

        [DataType(DataType.Time)]
        public DateTime HoraEntrada { get; set; }

        public List<NotaFiscal> Notas { get; set; } = new List<NotaFiscal>();

        public int EstacionamentoId { get; set; }
        public Estacionamento? Estacionamento { get; set; }
      
    }
}
