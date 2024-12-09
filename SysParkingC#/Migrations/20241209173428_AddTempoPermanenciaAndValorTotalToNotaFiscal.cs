using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysParkingC_.Migrations
{
    public partial class AddTempoPermanenciaAndValorTotalToNotaFiscal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TempoPermanencia",
                table: "NotaFiscal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValorTotal",
                table: "NotaFiscal",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempoPermanencia",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "NotaFiscal");
        }
    }
}
