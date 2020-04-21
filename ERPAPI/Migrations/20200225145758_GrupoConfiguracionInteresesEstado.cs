using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GrupoConfiguracionInteresesEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "GrupoConfiguracionIntereses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "GrupoConfiguracionIntereses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "GrupoConfiguracionIntereses");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "GrupoConfiguracionIntereses");
        }
    }
}
