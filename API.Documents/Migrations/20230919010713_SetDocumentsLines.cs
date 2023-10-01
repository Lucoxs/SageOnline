using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Documents.Migrations
{
    /// <inheritdoc />
    public partial class SetDocumentsLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lv_product_name",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "lv_variant_name",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "le_bundle_name",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "le_discount",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "le_unit_price",
                table: "le_document_line_bundle_element");

            migrationBuilder.DropColumn(
                name: "lb_bundle_name",
                table: "lb_document_line_bundle");

            migrationBuilder.AlterColumn<int>(
                name: "lv_variant_id",
                table: "lv_document_line_variant",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "lv_product_id",
                table: "lv_document_line_variant",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<double>(
                name: "lv_net_price",
                table: "lv_document_line_variant",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lb_discount",
                table: "lb_document_line_bundle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lb_net_price",
                table: "lb_document_line_bundle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lb_unit_price",
                table: "lb_document_line_bundle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "do_warehouse_destination_id",
                table: "do_document",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lv_net_price",
                table: "lv_document_line_variant");

            migrationBuilder.DropColumn(
                name: "lb_discount",
                table: "lb_document_line_bundle");

            migrationBuilder.DropColumn(
                name: "lb_net_price",
                table: "lb_document_line_bundle");

            migrationBuilder.DropColumn(
                name: "lb_unit_price",
                table: "lb_document_line_bundle");

            migrationBuilder.DropColumn(
                name: "do_warehouse_destination_id",
                table: "do_document");

            migrationBuilder.AlterColumn<long>(
                name: "lv_variant_id",
                table: "lv_document_line_variant",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "lv_product_id",
                table: "lv_document_line_variant",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "lv_product_name",
                table: "lv_document_line_variant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lv_variant_name",
                table: "lv_document_line_variant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "le_bundle_name",
                table: "le_document_line_bundle_element",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "le_discount",
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

            migrationBuilder.AddColumn<string>(
                name: "lb_bundle_name",
                table: "lb_document_line_bundle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
