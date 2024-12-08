namespace SysParkingC_.Models
{
    public class Relatorio
    {
        public int Id { get; set; }

        // Total de veículos estacionados no período
        public int TotalVeiculosEstacionados { get; set; }

        // Total de valores arrecadados no período
        public double TotalArrecadado { get; set; }

        // Tempo total de permanência em horas
        public double TempoTotalPermanenciaHoras { get; set; }

        // Período do relatório (ex: dia, semana, mês, ou intervalo personalizado)
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
    }
}
