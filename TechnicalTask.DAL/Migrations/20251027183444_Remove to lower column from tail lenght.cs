using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Removetolowercolumnfromtaillenght : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tail_length",
                table: "Dogs",
                newName: "TailLength");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TailLength",
                table: "Dogs",
                newName: "tail_length");
        }
    }
}
