using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class QuitarCampoTransferenciaInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceBranchName",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "TargetBranchName",
                table: "InventoryTransfer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceBranchName",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetBranchName",
                table: "InventoryTransfer",
                nullable: true);
        }
    }
}
