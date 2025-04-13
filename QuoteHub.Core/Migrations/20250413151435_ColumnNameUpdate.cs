using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteHub.Core.Migrations
{
    /// <inheritdoc />
    public partial class ColumnNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedon",
                table: "Quotes",
                newName: "updated_on");

            migrationBuilder.RenameColumn(
                name: "quotetext",
                table: "Quotes",
                newName: "quote_text");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "Quotes",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "createdon",
                table: "Quotes",
                newName: "created_on");

            migrationBuilder.RenameColumn(
                name: "updatedon",
                table: "Languages",
                newName: "updated_on");

            migrationBuilder.RenameColumn(
                name: "isocode",
                table: "Languages",
                newName: "iso_code");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "Languages",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "createdon",
                table: "Languages",
                newName: "created_on");

            migrationBuilder.RenameColumn(
                name: "updatedon",
                table: "Authors",
                newName: "updated_on");

            migrationBuilder.RenameColumn(
                name: "isactive",
                table: "Authors",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "dateofbirth",
                table: "Authors",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "createdon",
                table: "Authors",
                newName: "created_on");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_on",
                table: "Quotes",
                newName: "updatedon");

            migrationBuilder.RenameColumn(
                name: "quote_text",
                table: "Quotes",
                newName: "quotetext");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Quotes",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "created_on",
                table: "Quotes",
                newName: "createdon");

            migrationBuilder.RenameColumn(
                name: "updated_on",
                table: "Languages",
                newName: "updatedon");

            migrationBuilder.RenameColumn(
                name: "iso_code",
                table: "Languages",
                newName: "isocode");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Languages",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "created_on",
                table: "Languages",
                newName: "createdon");

            migrationBuilder.RenameColumn(
                name: "updated_on",
                table: "Authors",
                newName: "updatedon");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Authors",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "Authors",
                newName: "dateofbirth");

            migrationBuilder.RenameColumn(
                name: "created_on",
                table: "Authors",
                newName: "createdon");
        }
    }
}
