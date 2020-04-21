using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class GrupoConfiguracionIntereses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrupoConfiguracionInteresesId",
                table: "FundingInterestRate",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_FundingInterestRate_GrupoConfiguracionInteresesId",
                table: "FundingInterestRate",
                column: "GrupoConfiguracionInteresesId");

            migrationBuilder.AddForeignKey(
                name: "FK_FundingInterestRate_GrupoConfiguracionIntereses_GrupoConfiguracionInteresesId",
                table: "FundingInterestRate",
                column: "GrupoConfiguracionInteresesId",
                principalTable: "GrupoConfiguracionIntereses",
                principalColumn: "Id",
                
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundingInterestRate_GrupoConfiguracionIntereses_GrupoConfiguracionInteresesId",
                table: "FundingInterestRate");

            migrationBuilder.DropIndex(
                name: "IX_FundingInterestRate_GrupoConfiguracionInteresesId",
                table: "FundingInterestRate");

            migrationBuilder.DropColumn(
                name: "GrupoConfiguracionInteresesId",
                table: "FundingInterestRate");
        }
    }
}
