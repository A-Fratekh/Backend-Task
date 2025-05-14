using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editingRelationsOfOccupations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOccupations_Occupations_OccupationId1",
                table: "DepartmentOccupations");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentOccupations_OccupationId1",
                table: "DepartmentOccupations");

            migrationBuilder.DropColumn(
                name: "OccupationId1",
                table: "DepartmentOccupations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OccupationId1",
                table: "DepartmentOccupations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOccupations_OccupationId1",
                table: "DepartmentOccupations",
                column: "OccupationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOccupations_Occupations_OccupationId1",
                table: "DepartmentOccupations",
                column: "OccupationId1",
                principalTable: "Occupations",
                principalColumn: "Id");
        }
    }
}
