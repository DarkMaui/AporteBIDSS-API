using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Contratos3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

         

       

          

            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FundingInterestRate",
                table: "Contrato_detalle",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeDescuento",
                table: "Contrato_detalle",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Tax",
                table: "Contrato_detalle",
                nullable: false,
                defaultValue: 0.0);

           
            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxId",
                table: "Product",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Tax_TaxId",
                table: "Product",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Tax_TaxId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "RecipeDetail");

            migrationBuilder.DropIndex(
                name: "IX_Product_TaxId",
                table: "Product");

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
                name: "TaxId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FundingInterestRate",
                table: "Contrato_detalle");

            migrationBuilder.DropColumn(
                name: "PorcentajeDescuento",
                table: "Contrato_detalle");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Contrato_detalle");
        }
    }
}
