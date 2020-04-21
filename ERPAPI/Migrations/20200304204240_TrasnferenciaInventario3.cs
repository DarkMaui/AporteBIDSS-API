using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TrasnferenciaInventario3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceBranchName",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "TargetBranchName",
                table: "InventoryTransfer");
        }
    }
}
