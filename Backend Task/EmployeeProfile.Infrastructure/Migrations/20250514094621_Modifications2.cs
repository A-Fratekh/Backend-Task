using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modifications2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OccupationIds",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccupationIds",
                table: "Departments");
        }
    }
}
