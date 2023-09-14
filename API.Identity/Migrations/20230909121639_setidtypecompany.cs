using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Identity.Migrations
{
    /// <inheritdoc />
    public partial class setidtypecompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "co_id",
                table: "co_company",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "co_id",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "co_id",
                table: "co_company",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "co_id",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
