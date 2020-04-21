using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Campo_SaldoActual_NoRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoRecibo",
                table: "Contrato_movimientos",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaldoActual",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoRecibo",
                table: "Contrato_movimientos");

            migrationBuilder.DropColumn(
                name: "SaldoActual",
                table: "Contrato");
        }
    }
}
