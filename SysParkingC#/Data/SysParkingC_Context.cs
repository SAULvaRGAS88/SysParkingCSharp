using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysParkingC_.Models;

namespace SysParkingC_.Data
{
    public class SysParkingC_Context : DbContext
    {
        public SysParkingC_Context (DbContextOptions<SysParkingC_Context> options)
            : base(options)
        {
        }

        public DbSet<SysParkingC_.Models.Carro> Carro { get; set; } = default!;

        public DbSet<SysParkingC_.Models.Estacionamento>? Estacionamento { get; set; }

        public DbSet<SysParkingC_.Models.NotaFiscal>? NotaFiscal { get; set; }

        public DbSet<SysParkingC_.Models.Usuario>? Usuario { get; set; }

        public DbSet<SysParkingC_.Models.Relatorio>? Relatorio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaFiscal>()
                .HasOne(n => n.Carro)
                .WithOne(c => c.NotaFiscal)
                .HasForeignKey<NotaFiscal>(n => n.CarroId)
                .OnDelete(DeleteBehavior.SetNull); // Define que a referência será definida como NULL
        }
    }
}
