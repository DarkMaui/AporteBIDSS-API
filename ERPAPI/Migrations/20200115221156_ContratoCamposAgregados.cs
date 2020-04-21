
﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ContratoCamposAgregados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Finaciar",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);
                  
            migrationBuilder.AddColumn<double>(
                name: "Impuesto",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InteresesFinaciar",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrimaMonto",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaldoCredito",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SinFinaciar",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalContrato",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorContado",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorCuota",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorPrima",
                table: "Contrato",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finaciar",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "Impuesto",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "InteresesFinaciar",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "PrimaMonto",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "SaldoCredito",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "SinFinaciar",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "TotalContrato",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ValorContado",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ValorCuota",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ValorPrima",
                table: "Contrato");
        }
    }
}
