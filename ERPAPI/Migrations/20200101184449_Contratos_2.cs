using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Contratos_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipo_movimiento",
                table: "Contrato_movimientos",
                newName: "Tipo_movimiento");

            migrationBuilder.AddColumn<bool>(
                name: "FlagConsignacion",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Product",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serie",
                table: "Product",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerieChasis",
                table: "Product",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerieMotor",
                table: "Product",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Descuentos",
                table: "Contrato_detalle",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Impuestos",
                table: "Contrato_detalle",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Porcentaje_descuento",
                table: "Contrato_detalle",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "EmployeesId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_EmployeesId",
                table: "Contrato",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Employees_EmployeesId",
                table: "Contrato",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Employees_EmployeesId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_EmployeesId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "FlagConsignacion",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SerieChasis",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SerieMotor",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Descuentos",
                table: "Contrato_detalle");

            migrationBuilder.DropColumn(
                name: "Impuestos",
                table: "Contrato_detalle");

            migrationBuilder.DropColumn(
                name: "Porcentaje_descuento",
                table: "Contrato_detalle");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "Contrato");

            migrationBuilder.RenameColumn(
                name: "Tipo_movimiento",
                table: "Contrato_movimientos",
                newName: "tipo_movimiento");
        }
    }
}
