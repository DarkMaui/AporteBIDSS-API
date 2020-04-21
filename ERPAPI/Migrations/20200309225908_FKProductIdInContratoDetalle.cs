using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class FKProductIdInContratoDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contrato_detalle_ProductId",
                table: "Contrato_detalle",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_detalle_Product_ProductId",
                table: "Contrato_detalle",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_detalle_Product_ProductId",
                table: "Contrato_detalle");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_detalle_ProductId",
                table: "Contrato_detalle");
        }
    }
}
