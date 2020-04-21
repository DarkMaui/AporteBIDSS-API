using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class add_Contrato_movimientosId_Contrato_plan_pagos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Contrato_movimientosId",
                table: "Contrato_plan_pagos",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_plan_pagos_Contrato_movimientosId",
                table: "Contrato_plan_pagos",
                column: "Contrato_movimientosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_plan_pagos_Contrato_movimientos_Contrato_movimientosId",
                table: "Contrato_plan_pagos",
                column: "Contrato_movimientosId",
                principalTable: "Contrato_movimientos",
                principalColumn: "Contrato_movimientosId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_plan_pagos_Contrato_movimientos_Contrato_movimientosId",
                table: "Contrato_plan_pagos");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_plan_pagos_Contrato_movimientosId",
                table: "Contrato_plan_pagos");

            migrationBuilder.DropColumn(
                name: "Contrato_movimientosId",
                table: "Contrato_plan_pagos");
        }
    }
}
