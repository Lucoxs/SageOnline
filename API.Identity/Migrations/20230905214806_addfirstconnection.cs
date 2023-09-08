using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addfirstconnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstConnection",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstConnection",
                table: "AspNetUsers");
        }
    }
}
