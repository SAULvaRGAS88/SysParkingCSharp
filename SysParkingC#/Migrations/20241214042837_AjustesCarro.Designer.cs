﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SysParkingC_.Data;

#nullable disable

namespace SysParkingC_.Migrations
{
    [DbContext(typeof(SysParkingC_Context))]
    [Migration("20241214042837_AjustesCarro")]
    partial class AjustesCarro
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SysParkingC_.Models.Carro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<int>("EstacionamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoraEntrada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.HasIndex("EstacionamentoId");

                    b.ToTable("Carro");
                });

            modelBuilder.Entity("SysParkingC_.Models.Estacionamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroVagasDisponiveis")
                        .HasColumnType("int");

                    b.Property<double>("Preco15Min")
                        .HasColumnType("float");

                    b.Property<double>("Preco1Hora")
                        .HasColumnType("float");

                    b.Property<double>("Preco30Min")
                        .HasColumnType("float");

                    b.Property<double>("PrecoDiaria")
                        .HasColumnType("float");

                    b.Property<double>("PrecoMensal")
                        .HasColumnType("float");

                    b.Property<double>("PrecoPernoite")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Estacionamento");
                });

            modelBuilder.Entity("SysParkingC_.Models.NotaFiscal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarroId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EstacionamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoraSaida")
                        .HasColumnType("datetime2");

                    b.Property<int>("Pagamento")
                        .HasColumnType("int");

                    b.Property<string>("TempoPermanencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ValorTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CarroId")
                        .IsUnique()
                        .HasFilter("[CarroId] IS NOT NULL");

                    b.HasIndex("EstacionamentoId");

                    b.ToTable("NotaFiscal");
                });

            modelBuilder.Entity("SysParkingC_.Models.Relatorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("PeriodoFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PeriodoInicio")
                        .HasColumnType("datetime2");

                    b.Property<double>("TempoTotalPermanenciaHoras")
                        .HasColumnType("float");

                    b.Property<double>("TotalArrecadado")
                        .HasColumnType("float");

                    b.Property<int>("TotalVeiculosEstacionados")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Relatorio");
                });

            modelBuilder.Entity("SysParkingC_.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstacionamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstacionamentoId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("SysParkingC_.Models.Carro", b =>
                {
                    b.HasOne("SysParkingC_.Models.Estacionamento", "Estacionamento")
                        .WithMany()
                        .HasForeignKey("EstacionamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estacionamento");
                });

            modelBuilder.Entity("SysParkingC_.Models.NotaFiscal", b =>
                {
                    b.HasOne("SysParkingC_.Models.Carro", "Carro")
                        .WithOne("NotaFiscal")
                        .HasForeignKey("SysParkingC_.Models.NotaFiscal", "CarroId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SysParkingC_.Models.Estacionamento", null)
                        .WithMany("Notas")
                        .HasForeignKey("EstacionamentoId");

                    b.Navigation("Carro");
                });

            modelBuilder.Entity("SysParkingC_.Models.Usuario", b =>
                {
                    b.HasOne("SysParkingC_.Models.Estacionamento", "Estacionamento")
                        .WithMany("Usuarios")
                        .HasForeignKey("EstacionamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estacionamento");
                });

            modelBuilder.Entity("SysParkingC_.Models.Carro", b =>
                {
                    b.Navigation("NotaFiscal");
                });

            modelBuilder.Entity("SysParkingC_.Models.Estacionamento", b =>
                {
                    b.Navigation("Notas");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
