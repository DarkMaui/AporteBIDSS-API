using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class TrasnferenciaInventario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventoryTransferLine",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "InventoryTransferLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacion",
                table: "InventoryTransferLine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacion",
                table: "InventoryTransferLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "InventoryTransferLine");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "InventoryTransferLine");
        }
    }
}
