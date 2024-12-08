namespace SysParkingC_.Models
{
    public class Estacionamento
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Endereco { get; set; }
        public int NumeroVagasDisponiveis { get; set; }

        // Lista de usuários associados ao estacionamento
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        // Lista de notas fiscais associadas a este estacionamento
        public List<NotaFiscal> Notas { get; set; } = new List<NotaFiscal>();

        // Relacionamento com a tabela de preços
        public int? TabelaPrecoId { get; set; } // FK
        public TabelaPreco? TabelaPreco { get; set; } // Navegação
    }

    public class TabelaPreco
    {
        public int Id { get; set; }

        public double Preco15Min { get; set; }
        public double Preco30Min { get; set; }
        public double Preco1Hora { get; set; }
        public double PrecoDiaria { get; set; }
        public double PrecoPernoite { get; set; }
        public double PrecoMensal { get; set; }

        // Lista de estacionamentos associados a esta tabela
        public List<Estacionamento> Estacionamentos { get; set; } = new List<Estacionamento>();
    }
}
