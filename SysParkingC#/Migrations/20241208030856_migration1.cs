using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysParkingC_.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatorio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalVeiculosEstacionados = table.Column<int>(type: "int", nullable: false),
                    TotalArrecadado = table.Column<double>(type: "float", nullable: false),
                    TempoTotalPermanenciaHoras = table.Column<double>(type: "float", nullable: false),
                    PeriodoInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodoFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaPreco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preco15Min = table.Column<double>(type: "float", nullable: false),
                    Preco30Min = table.Column<double>(type: "float", nullable: false),
                    Preco1Hora = table.Column<double>(type: "float", nullable: false),
                    PrecoDiaria = table.Column<double>(type: "float", nullable: false),
                    PrecoPernoite = table.Column<double>(type: "float", nullable: false),
                    PrecoMensal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPreco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estacionamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroVagasDisponiveis = table.Column<int>(type: "int", nullable: false),
                    TabelaPrecoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estacionamento_TabelaPreco_TabelaPrecoId",
                        column: x => x.TabelaPrecoId,
                        principalTable: "TabelaPreco",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarroId = table.Column<int>(type: "int", nullable: false),
                    EstacionamentoId = table.Column<int>(type: "int", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_Carro_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalTable: "Estacionamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstacionamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalTable: "Estacionamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamento_TabelaPrecoId",
                table: "Estacionamento",
                column: "TabelaPrecoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_CarroId",
                table: "NotaFiscal",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_EstacionamentoId",
                table: "NotaFiscal",
                column: "EstacionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EstacionamentoId",
                table: "Usuario",
                column: "EstacionamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "Relatorio");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Carro");

            migrationBuilder.DropTable(
                name: "Estacionamento");

            migrationBuilder.DropTable(
                name: "TabelaPreco");
        }
    }
}
