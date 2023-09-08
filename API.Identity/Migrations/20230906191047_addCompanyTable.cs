using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FirstConnection",
                table: "AspNetUsers",
                newName: "us_firstconnection");

            migrationBuilder.AddColumn<long>(
                name: "co_id",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "us_firstname",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "us_lastname",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "co_company",
                columns: table => new
                {
                    co_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    co_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    co_activity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_legal_status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_capital = table.Column<double>(type: "float", nullable: true),
                    co_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_complement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_zip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_city = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_region = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_siret = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_vat_identifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_naf_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    co_max_users = table.Column<int>(type: "int", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_company", x => x.co_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_co_id",
                table: "AspNetUsers",
                column: "co_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_co_company_co_id",
                table: "AspNetUsers",
                column: "co_id",
                principalTable: "co_company",
                principalColumn: "co_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_co_company_co_id",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "co_company");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_co_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "co_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "us_firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "us_lastname",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "us_firstconnection",
                table: "AspNetUsers",
                newName: "FirstConnection");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
