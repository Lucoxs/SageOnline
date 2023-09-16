using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Documents.Migrations
{
    /// <inheritdoc />
    public partial class SetDocumentIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_DocumentNumber_DocumentType",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CompanyId_DocumentNumber_DocumentType",
                table: "Documents",
                columns: new[] { "CompanyId", "DocumentNumber", "DocumentType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_CompanyId_DocumentNumber_DocumentType",
                table: "Documents");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentNumber_DocumentType",
                table: "Documents",
                columns: new[] { "DocumentNumber", "DocumentType" },
                unique: true);
        }
    }
}
