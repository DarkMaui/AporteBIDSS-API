using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Add_campos_de_mora_Contrato_Movimientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dias_mora",
                table: "Contrato_movimientos",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "InteresesMora",
                table: "Contrato_movimientos",
                nullable: true,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PagoPorMora",
                table: "Contrato_movimientos",
                nullable: true,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valorpagado",
                table: "Contrato_movimientos",
                nullable: true,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dias_mora",
                table: "Contrato_movimientos");

            migrationBuilder.DropColumn(
                name: "InteresesMora",
                table: "Contrato_movimientos");

            migrationBuilder.DropColumn(
                name: "PagoPorMora",
                table: "Contrato_movimientos");

            migrationBuilder.DropColumn(
                name: "Valorpagado",
                table: "Contrato_movimientos");
        }
    }
}
