using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadXmlInvoce.Migrations
{
    /// <inheritdoc />
    public partial class sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Invoices_invoceNumber",
                table: "products");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Invoices_invoceNumber",
                table: "products",
                column: "invoceNumber",
                principalTable: "Invoices",
                principalColumn: "numDock",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Invoices_invoceNumber",
                table: "products");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Invoices_invoceNumber",
                table: "products",
                column: "invoceNumber",
                principalTable: "Invoices",
                principalColumn: "numDock");
        }
    }
}
