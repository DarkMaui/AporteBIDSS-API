using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Campos_de_mora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<double>(
                name: "InteresesMora",
                table: "Contrato",
                nullable: true,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PagoPorMora",
                table: "Contrato",
                nullable: true,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Valorpagado",
                table: "Contrato",
                nullable: true,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "InteresesMora",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "PagoPorMora",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "Valorpagado",
                table: "Contrato");
        }
    }
}
