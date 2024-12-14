using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysParkingC_.Migrations
{
    public partial class AjustesCarro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Carro_CarroId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_CarroId",
                table: "NotaFiscal");

            migrationBuilder.AlterColumn<int>(
                name: "CarroId",
                table: "NotaFiscal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Carro",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_CarroId",
                table: "NotaFiscal",
                column: "CarroId",
                unique: true,
                filter: "[CarroId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Carro_CarroId",
                table: "NotaFiscal",
                column: "CarroId",
                principalTable: "Carro",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Carro_CarroId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_CarroId",
                table: "NotaFiscal");

            migrationBuilder.AlterColumn<int>(
                name: "CarroId",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Carro",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_CarroId",
                table: "NotaFiscal",
                column: "CarroId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Carro_CarroId",
                table: "NotaFiscal",
                column: "CarroId",
                principalTable: "Carro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
