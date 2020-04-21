using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class ProdcutNUmTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "Valor_prima",
              table: "Product");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultSellingPrice",
                table: "Product",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultBuyingPrice",
                table: "Product",
                nullable: false,
                oldClrType: typeof(double));

            

            migrationBuilder.Sql(@"ALTER TABLE [Product] ADD [Valor_prima] AS ([DefaultSellingPrice]* ([Prima]/100))");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "Valor_prima",
              table: "Product");

            migrationBuilder.AlterColumn<double>(
                name: "DefaultSellingPrice",
                table: "Product",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "DefaultBuyingPrice",
                table: "Product",
                nullable: false,
                oldClrType: typeof(decimal));

            

            migrationBuilder.Sql(@"ALTER TABLE [Product] ADD [Valor_prima] AS ([DefaultSellingPrice]* ([Prima]/100))");

        }
    }
}
