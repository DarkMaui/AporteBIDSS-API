using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ModificacionProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupo_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Linea_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marca_MarcaId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LineaId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GrupoId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FundingInterestRateId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeDescuento",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerieChasis",
                table: "Contrato_detalle",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerieMotor",
                table: "Contrato_detalle",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                table: "Product",
                column: "FundingInterestRateId",
                principalTable: "FundingInterestRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Grupo_GrupoId",
                table: "Product",
                column: "GrupoId",
                principalTable: "Grupo",
                principalColumn: "GrupoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Linea_LineaId",
                table: "Product",
                column: "LineaId",
                principalTable: "Linea",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marca_MarcaId",
                table: "Product",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Grupo_GrupoId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Linea_LineaId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marca_MarcaId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PorcentajeDescuento",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SerieChasis",
                table: "Contrato_detalle");

            migrationBuilder.DropColumn(
                name: "SerieMotor",
                table: "Contrato_detalle");

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LineaId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GrupoId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FundingInterestRateId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                table: "Product",
                column: "FundingInterestRateId",
                principalTable: "FundingInterestRate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Grupo_GrupoId",
                table: "Product",
                column: "GrupoId",
                principalTable: "Grupo",
                principalColumn: "GrupoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Linea_LineaId",
                table: "Product",
                column: "LineaId",
                principalTable: "Linea",
                principalColumn: "LineaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marca_MarcaId",
                table: "Product",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "MarcaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
