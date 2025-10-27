using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fixbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tail_lenght",
                table: "Dogs",
                newName: "tail_length");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tail_length",
                table: "Dogs",
                newName: "tail_lenght");
        }
    }
}
