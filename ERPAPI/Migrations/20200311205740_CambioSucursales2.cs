using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambioSucursales2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Currency_CurrencyId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Branch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Branch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Branch",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Currency_CurrencyId",
                table: "Branch",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
