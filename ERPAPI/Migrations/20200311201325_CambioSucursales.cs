using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambioSucursales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Branch_BranchCode",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "BranchCode",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Branch");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Branch",
                newName: "BranchType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BranchType",
                table: "Branch",
                newName: "URL");

            migrationBuilder.AddColumn<string>(
                name: "BranchCode",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Branch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Branch",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchCode",
                table: "Branch",
                column: "BranchCode",
                unique: true,
                filter: "[BranchCode] IS NOT NULL");
        }
    }
}
