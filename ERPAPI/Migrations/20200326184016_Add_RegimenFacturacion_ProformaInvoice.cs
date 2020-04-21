using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Add_RegimenFacturacion_ProformaInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroExonerado",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroOC",
                table: "ProformaInvoice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroSAG",
                table: "ProformaInvoice",
                nullable: true);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroExonerado",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "NumeroOC",
                table: "ProformaInvoice");

            migrationBuilder.DropColumn(
                name: "NumeroSAG",
                table: "ProformaInvoice");

        
        }
    }
}
