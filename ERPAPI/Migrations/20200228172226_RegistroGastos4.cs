using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RegistroGastos4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConceptoGasto",
                table: "RegistroGastos",
                newName: "Detalle");

            migrationBuilder.AddColumn<string>(
                name: "Concepto",
                table: "RegistroGastos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concepto",
                table: "RegistroGastos");

            migrationBuilder.RenameColumn(
                name: "Detalle",
                table: "RegistroGastos",
                newName: "ConceptoGasto");
        }
    }
}
