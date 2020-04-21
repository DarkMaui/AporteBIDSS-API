using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RegistroGastos3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "RegistroGastos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "RegistroGastos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "RegistroGastos");

            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "RegistroGastos");
        }
    }
}
