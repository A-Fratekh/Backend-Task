using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GradeId1",
                table: "OccupationGrades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OccupationId1",
                table: "OccupationGrades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OccupationGrades_GradeId1",
                table: "OccupationGrades",
                column: "GradeId1");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationGrades_OccupationId1",
                table: "OccupationGrades",
                column: "OccupationId1");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropIndex(
                name: "IX_OccupationGrades_GradeId1",
                table: "OccupationGrades");

            migrationBuilder.DropIndex(
                name: "IX_OccupationGrades_OccupationId1",
                table: "OccupationGrades");

            migrationBuilder.DropColumn(
                name: "GradeId1",
                table: "OccupationGrades");

            migrationBuilder.DropColumn(
                name: "OccupationId1",
                table: "OccupationGrades");
        }
    }
}
