using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Educationalprogramsremovecoursetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseType",
                table: "EducationPrograms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseType",
                table: "EducationPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
