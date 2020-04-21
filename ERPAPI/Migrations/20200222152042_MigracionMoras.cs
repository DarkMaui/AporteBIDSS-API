using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class MigracionMoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_plan_pagos_Contrato_movimientos_Contrato_movimientosId",
                table: "Contrato_plan_pagos");

            migrationBuilder.AlterColumn<long>(
                name: "Contrato_movimientosId",
                table: "Contrato_plan_pagos",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateTable(
                name: "Moras",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(maxLength: 5000, nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    MinDias = table.Column<int>(nullable: false),
                    MaxDias = table.Column<int>(nullable: false),
                    Intereses = table.Column<int>(nullable: false),
                    Requerido = table.Column<bool>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    MorasId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moras_Moras_MorasId",
                        column: x => x.MorasId,
                        principalTable: "Moras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Moras_MorasId",
                table: "Moras",
                column: "MorasId");

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

            migrationBuilder.DropTable(
                name: "Moras");

            migrationBuilder.AlterColumn<long>(
                name: "Contrato_movimientosId",
                table: "Contrato_plan_pagos",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_plan_pagos_Contrato_movimientos_Contrato_movimientosId",
                table: "Contrato_plan_pagos",
                column: "Contrato_movimientosId",
                principalTable: "Contrato_movimientos",
                principalColumn: "Contrato_movimientosId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
