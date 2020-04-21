using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class RegistroGastos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistroGastos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: false),
                    ConceptoGasto = table.Column<string>(nullable: true),
                    Identidad = table.Column<int>(nullable: false),
                    TipoGastosId = table.Column<int>(nullable: false),
                    Documento = table.Column<string>(nullable: true),
                    monto = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroGastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroGastos_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroGastos_TipoGastos_TipoGastosId",
                        column: x => x.TipoGastosId,
                        principalTable: "TipoGastos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroGastos_BranchId",
                table: "RegistroGastos",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroGastos_TipoGastosId",
                table: "RegistroGastos",
                column: "TipoGastosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroGastos");
        }
    }
}
