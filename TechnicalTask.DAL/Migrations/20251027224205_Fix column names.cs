using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fixcolumnnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Dogs",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dogs",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Dogs",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dogs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TailLength",
                table: "Dogs",
                newName: "tail_lenght");

            migrationBuilder.RenameIndex(
                name: "IX_Dogs_Name",
                table: "Dogs",
                newName: "IX_Dogs_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Dogs",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Dogs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "Dogs",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Dogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "tail_lenght",
                table: "Dogs",
                newName: "TailLength");

            migrationBuilder.RenameIndex(
                name: "IX_Dogs_name",
                table: "Dogs",
                newName: "IX_Dogs_Name");
        }
    }
}
