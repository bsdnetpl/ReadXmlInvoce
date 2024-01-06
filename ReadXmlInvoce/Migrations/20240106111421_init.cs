using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadXmlInvoce.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    numDock = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    dateSell = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.numDock);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lp = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nameProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxVat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    valueTax = table.Column<double>(type: "float", nullable: false),
                    invoceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_Invoices_invoceNumber",
                        column: x => x.invoceNumber,
                        principalTable: "Invoices",
                        principalColumn: "numDock");
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_invoceNumber",
                table: "products",
                column: "invoceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
