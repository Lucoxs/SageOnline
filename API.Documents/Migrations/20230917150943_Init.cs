using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Documents.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "do_document",
                columns: table => new
                {
                    do_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    do_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    co_id = table.Column<int>(type: "int", nullable: false),
                    co_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_hash_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    do_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    do_hash_created_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    do_modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    do_hash_modified_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    do_deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    do_hash_deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    do_is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    us_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    do_document_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    do_hash_document_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    do_document_type = table.Column<int>(type: "int", nullable: false),
                    do_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    do_shipping_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    do_shipping_address_id = table.Column<int>(type: "int", nullable: false),
                    do_warehouse_id = table.Column<int>(type: "int", nullable: false),
                    do_third_account_id = table.Column<int>(type: "int", nullable: false),
                    do_contact_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_do_document", x => x.do_id);
                });

            migrationBuilder.CreateTable(
                name: "dl_document_line",
                columns: table => new
                {
                    dl_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dl_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    co_id = table.Column<int>(type: "int", nullable: false),
                    co_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_hash_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dl_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dl_hash_created_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dl_modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dl_hash_modified_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dl_deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dl_hash_deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dl_is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    us_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dl_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dl_is_bundle = table.Column<bool>(type: "bit", nullable: false),
                    dl_unit_price = table.Column<double>(type: "float", nullable: false),
                    dl_quantity = table.Column<int>(type: "int", nullable: false),
                    dl_discount = table.Column<double>(type: "float", nullable: false),
                    dl_total_price = table.Column<double>(type: "float", nullable: false),
                    do_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dl_document_line", x => x.dl_id);
                    table.ForeignKey(
                        name: "FK_dl_document_line_do_document_do_id",
                        column: x => x.do_id,
                        principalTable: "do_document",
                        principalColumn: "do_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lb_document_line_bundle",
                columns: table => new
                {
                    lb_id = table.Column<long>(type: "bigint", nullable: false),
                    lb_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    co_id = table.Column<int>(type: "int", nullable: false),
                    co_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_hash_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lb_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lb_hash_created_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lb_modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lb_hash_modified_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lb_deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lb_hash_deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lb_is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    us_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lb_bundle_id = table.Column<int>(type: "int", nullable: false),
                    lb_bundle_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lb_document_line_bundle", x => x.lb_id);
                    table.ForeignKey(
                        name: "FK_lb_document_line_bundle_dl_document_line_lb_id",
                        column: x => x.lb_id,
                        principalTable: "dl_document_line",
                        principalColumn: "dl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lv_document_line_variant",
                columns: table => new
                {
                    lv_id = table.Column<long>(type: "bigint", nullable: false),
                    lv_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    co_id = table.Column<int>(type: "int", nullable: false),
                    co_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_hash_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lv_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lv_hash_created_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lv_modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lv_hash_modified_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lv_deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lv_hash_deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lv_is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    us_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lv_product_id = table.Column<long>(type: "bigint", nullable: false),
                    lv_product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lv_variant_id = table.Column<long>(type: "bigint", nullable: false),
                    lv_variant_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lv_document_line_variant", x => x.lv_id);
                    table.ForeignKey(
                        name: "FK_lv_document_line_variant_dl_document_line_lv_id",
                        column: x => x.lv_id,
                        principalTable: "dl_document_line",
                        principalColumn: "dl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "le_document_line_bundle_element",
                columns: table => new
                {
                    le_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    le_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    co_id = table.Column<int>(type: "int", nullable: false),
                    co_hash_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    us_hash_creater_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    le_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    le_hash_created_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    le_modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    le_hash_modified_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_modified_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    le_deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    le_hash_deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    le_is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    us_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    us_hash_deleter_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    le_bundle_id = table.Column<int>(type: "int", nullable: false),
                    le_bundle_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lb_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_le_document_line_bundle_element", x => x.le_id);
                    table.ForeignKey(
                        name: "FK_le_document_line_bundle_element_lb_document_line_bundle_lb_id",
                        column: x => x.lb_id,
                        principalTable: "lb_document_line_bundle",
                        principalColumn: "lb_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dl_document_line_do_id",
                table: "dl_document_line",
                column: "do_id");

            migrationBuilder.CreateIndex(
                name: "IX_do_document_co_id_do_document_number_do_document_type",
                table: "do_document",
                columns: new[] { "co_id", "do_document_number", "do_document_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_le_document_line_bundle_element_lb_id",
                table: "le_document_line_bundle_element",
                column: "lb_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "le_document_line_bundle_element");

            migrationBuilder.DropTable(
                name: "lv_document_line_variant");

            migrationBuilder.DropTable(
                name: "lb_document_line_bundle");

            migrationBuilder.DropTable(
                name: "dl_document_line");

            migrationBuilder.DropTable(
                name: "do_document");
        }
    }
}
