using System.ComponentModel.DataAnnotations.Schema;

namespace SysParkingC_.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string ? Marca { get; set; }
        public string ? Modelo { get; set; }
        public string ? Cor { get; set; }
        public string  ? Placa { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime HoraEntrada { get; set; }
        public List<NotaFiscal> Notas { get; set; } = new List<NotaFiscal>();

        public int EstacionamentoId { get; set; }
        public Estacionamento? Estacionamento { get; set; }
    }
}
