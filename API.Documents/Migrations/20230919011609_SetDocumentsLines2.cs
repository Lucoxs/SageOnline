using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Documents.Migrations
{
    /// <inheritdoc />
    public partial class SetDocumentsLines2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "le_quantity",
                table: "le_document_line_bundle_element");

            migrationBuilder.AddColumn<int>(
                name: "lb_quantity",
                table: "lb_document_line_bundle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lb_quantity",
                table: "lb_document_line_bundle");

            migrationBuilder.AddColumn<int>(
                name: "le_quantity",
                table: "le_document_line_bundle_element",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
