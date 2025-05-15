using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedEntitiesRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GradeIds",
                table: "Occupations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");



            migrationBuilder.AddColumn<string>(
                name: "OccupationIds",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

           

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {



            migrationBuilder.DropColumn(
                name: "GradeIds",
                table: "Occupations");

            migrationBuilder.DropColumn(
                name: "OccupationIds",
                table: "Grades");
        }
    }
}
