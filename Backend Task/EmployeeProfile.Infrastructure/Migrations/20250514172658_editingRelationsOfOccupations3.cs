using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editingRelationsOfOccupations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentIds",
                table: "Occupations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentIds",
                table: "Occupations");
        }
    }
}
