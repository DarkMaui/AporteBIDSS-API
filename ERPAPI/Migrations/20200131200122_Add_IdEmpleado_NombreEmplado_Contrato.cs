using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Add_IdEmpleado_NombreEmplado_Contrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdEmpleado",
                table: "Contrato",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpleado",
                table: "Contrato",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEmpleado",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "NombreEmpleado",
                table: "Contrato");
        }
    }
}
