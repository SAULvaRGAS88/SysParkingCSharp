using Microsoft.EntityFrameworkCore;

namespace SysParkingC_.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Estacionamento> Estacionamentos { get; set; }
    }
}
