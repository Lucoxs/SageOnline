using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Documents.Migrations
{
    /// <inheritdoc />
    public partial class SetPQDT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dl_discount",
                table: "dl_document_line");

            migrationBuilder.DropColumn(
                name: "dl_quantity",
                table: "dl_document_line");

            migrationBuilder.DropColumn(
                name: "dl_total_price",
                table: "dl_document_line");

            migrationBuilder.DropColumn(
                name: "dl_unit_price",
                table: "dl_document_line");

            migrationBuilder.AddColumn<double>(
                name: "lv_discount",
                table: "lv_document_line_variant",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "lv_quantity",
                table: "lv_document_line_variant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "lv_total_price",
                table: "lv_document_line_variant",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lv_unit_price",
                table: "lv_document_line_variant",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "le_discount",
                table: "le_document_line_bundle_element",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "le_quantity",
                table: "le_document_line_bundle_element",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "le_total_price",
                table: "le_document_line_bundle_element",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "le_unit_price",
                table: "le_document_line_bundle_element",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lb_total_price",
                table: "lb_document_line_bundle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lv_discount",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "lv_quantity",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "lv_total_price",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "lv_unit_price",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "le_discount",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "le_quantity",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "le_total_price",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "le_unit_price",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "lb_total_price",
                table: "lb_document_line_bundle");

            migrationBuilder.AddColumn<double>(
                name: "dl_discount",
                table: "dl_document_line",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "dl_quantity",
                table: "dl_document_line",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "dl_total_price",
                table: "dl_document_line",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "dl_unit_price",
                table: "dl_document_line",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
