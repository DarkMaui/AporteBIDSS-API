using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TrasnferenciaInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Currency_CurrencyId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_Estados_EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransfer_NumeracionSAR_NumeracionSARId",
                table: "InventoryTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransferLine_UnitOfMeasure_UnitOfMeasureId",
                table: "InventoryTransferLine");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransferLine_UnitOfMeasureId",
                table: "InventoryTransferLine");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_CurrencyId",
                table: "InventoryTransfer");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.DropIndex(
                name: "IX_InventoryTransfer_NumeracionSARId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "QtyIn",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "QtyOut",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureName",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "InventoryTransfer");

            migrationBuilder.DropColumn(
                name: "NumeracionSARId",
                table: "InventoryTransfer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QtyIn",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QtyOut",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasureName",
                table: "InventoryTransferLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "InventoryTransfer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "InventoryTransfer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstadoId",
                table: "InventoryTransfer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NumeracionSARId",
                table: "InventoryTransfer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_UnitOfMeasureId",
                table: "InventoryTransferLine",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_CurrencyId",
                table: "InventoryTransfer",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_NumeracionSARId",
                table: "InventoryTransfer",
                column: "NumeracionSARId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_Currency_CurrencyId",
                table: "InventoryTransfer",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_Estados_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "IdEstado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransfer_NumeracionSAR_NumeracionSARId",
                table: "InventoryTransfer",
                column: "NumeracionSARId",
                principalTable: "NumeracionSAR",
                principalColumn: "IdNumeracion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransferLine_UnitOfMeasure_UnitOfMeasureId",
                table: "InventoryTransferLine",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
