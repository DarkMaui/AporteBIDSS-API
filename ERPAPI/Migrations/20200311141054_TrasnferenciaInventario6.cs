using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TrasnferenciaInventario6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEstado",
                table: "InventoryTransfer");

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_Estados_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Estados_EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.AddColumn<int>(
                name: "IdEstado",
                table: "InventoryTransfer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
