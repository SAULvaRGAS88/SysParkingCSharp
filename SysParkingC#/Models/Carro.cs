using Microsoft.EntityFrameworkCore;
using SysParkingC_.Data;
using System.ComponentModel.DataAnnotations;

namespace SysParkingC_.Models
{
    public class Carro
    {
        private readonly SysParkingC_Context? _context; // Contexto do banco de dados

        // Construtor padrão necessário para model binding
        public Carro() { }

        // Construtor adicional para injeção do contexto
        public Carro(SysParkingC_Context context)
        {
            _context = context;
        }

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

        public async Task CarregarEstacionamentoAsync()
        {
            if (_context == null)
            {
                Console.WriteLine("Erro: o contexto não foi configurado.");
                return;
            }

            try
            {
                Estacionamento = await _context.Estacionamento
                    .FirstOrDefaultAsync(e => e.Id == this.EstacionamentoId);

                if (Estacionamento == null)
                {
                    Console.WriteLine($"Erro: estacionamento com ID {this.EstacionamentoId} não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar estacionamento: {ex.Message}");
            }
        }

        // Propriedade para calcular tempo de permanência em minutos
        public double TempoPermanencia
        {
            get
            {
                var horaAtual = DateTime.Now;
                var horaEntrada = DateTime.Today.Add(HoraEntrada.TimeOfDay);

                var tempoDePermanencia = horaAtual - horaEntrada;
                return Math.Round(tempoDePermanencia.TotalMinutes);
            }
        }

        public double ValorTotal => CalculaCustoPermanencia();

        private double CalculaCustoPermanencia()
        {
            if (Estacionamento == null)
            {
                Console.WriteLine("Estacionamento não está carregado.");
                return 0;
            }

            var tempoEmMinutos = TempoPermanencia;

            if (tempoEmMinutos <= 15) return Estacionamento.Preco15Min;
            if (tempoEmMinutos <= 30) return Estacionamento.Preco30Min;
            if (tempoEmMinutos <= 60) return Estacionamento.Preco1Hora;
            if (tempoEmMinutos > 60 && tempoEmMinutos <= 24 * 60) return Estacionamento.PrecoDiaria;
            if (tempoEmMinutos > 24 * 60) return Estacionamento.PrecoPernoite;

            return 0;
        }
    }
}
