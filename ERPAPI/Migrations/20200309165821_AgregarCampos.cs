using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AgregarCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "KardexViale",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfDocumentId",
                table: "KardexViale",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfDocumentName",
                table: "KardexViale",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CantidadRecibida",
                table: "InventoryTransferLine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "InventoryTransferLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "KardexViale");

            migrationBuilder.DropColumn(
                name: "TypeOfDocumentId",
                table: "KardexViale");

            migrationBuilder.DropColumn(
                name: "TypeOfDocumentName",
                table: "KardexViale");

            migrationBuilder.DropColumn(
                name: "CantidadRecibida",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "InventoryTransferLine");
        }
    }
}
