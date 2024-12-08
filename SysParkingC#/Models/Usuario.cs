namespace SysParkingC_.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string ? Nome { get; set; }
        public string ? Telefone { get; set; }
        public string ? Endereco { get; set; }
        public string ? Senha { get; set; }
        public string ? Email { get; set; }
        public int EstacionamentoId { get; set; }
        public Estacionamento ? Estacionamento { get; set; }
    }
}
