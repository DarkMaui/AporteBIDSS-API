using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TrasnferenciaInventario5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "InventoryTransfer");

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "InventoryTransfer",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "InventoryTransfer");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "InventoryTransfer",
                nullable: true);
        }
    }
}
