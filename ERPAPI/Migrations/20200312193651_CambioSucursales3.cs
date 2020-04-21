using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class CambioSucursales3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Branch");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Branch",
                nullable: false,
                defaultValue: 22);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_EmployeeId",
                table: "Branch",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Employees_EmployeeId",
                table: "Branch",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Employees_EmployeeId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_EmployeeId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Branch");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Branch",
                nullable: true);
        }
    }
}
