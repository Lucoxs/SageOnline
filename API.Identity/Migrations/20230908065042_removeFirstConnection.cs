using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Identity.Migrations
{
    /// <inheritdoc />
    public partial class removeFirstConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "us_firstconnection",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "us_firstconnection",
                table: "AspNetUsers",
                type: "bit",
                maxLength: 255,
                nullable: false,
                defaultValue: false);
        }
    }
}
