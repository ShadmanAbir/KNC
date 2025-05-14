using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Educationalprogramsfixedshortname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "EducationPrograms",
                newName: "ShortName");

            migrationBuilder.AlterColumn<string>(
                name: "StudentCode",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "EducationPrograms",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "StudentCode",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
