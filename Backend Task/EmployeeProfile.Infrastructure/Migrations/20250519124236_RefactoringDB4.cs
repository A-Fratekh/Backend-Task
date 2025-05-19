using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringDB4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentIds",
                table: "Occupations");

            migrationBuilder.DropColumn(
                name: "GradeIds",
                table: "Occupations");

            migrationBuilder.DropColumn(
                name: "OccupationIds",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "OccupationIds",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentIds",
                table: "Occupations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GradeIds",
                table: "Occupations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OccupationIds",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OccupationIds",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
