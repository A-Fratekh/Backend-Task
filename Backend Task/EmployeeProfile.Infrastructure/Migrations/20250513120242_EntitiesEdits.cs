using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Occupations_OccupationId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Occupations_Departments_DepartmentId",
                table: "Occupations");

            migrationBuilder.DropIndex(
                name: "IX_Occupations_DepartmentId",
                table: "Occupations");

            migrationBuilder.DropIndex(
                name: "IX_Grades_OccupationId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Occupations");

            migrationBuilder.DropColumn(
                name: "OccupationId",
                table: "Grades");

            migrationBuilder.CreateTable(
                name: "DepartmentOccupation",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccupationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentOccupation", x => new { x.DepartmentId, x.OccupationID });
                    table.ForeignKey(
                        name: "FK_DepartmentOccupation_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentOccupation_Occupations_OccupationID",
                        column: x => x.OccupationID,
                        principalTable: "Occupations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OccupationGrade",
                columns: table => new
                {
                    OccupationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationGrade", x => new { x.GradeId, x.OccupationId });
                    table.ForeignKey(
                        name: "FK_OccupationGrade_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OccupationGrade_Occupations_OccupationId",
                        column: x => x.OccupationId,
                        principalTable: "Occupations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GradeId",
                table: "Employees",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OccupationId",
                table: "Employees",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOccupation_OccupationID",
                table: "DepartmentOccupation",
                column: "OccupationID");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationGrade_OccupationId",
                table: "OccupationGrade",
                column: "OccupationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Grades_GradeId",
                table: "Employees",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Occupations_OccupationId",
                table: "Employees",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Grades_GradeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Occupations_OccupationId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "DepartmentOccupation");

            migrationBuilder.DropTable(
                name: "OccupationGrade");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GradeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OccupationId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Occupations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OccupationId",
                table: "Grades",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_DepartmentId",
                table: "Occupations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_OccupationId",
                table: "Grades",
                column: "OccupationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Occupations_OccupationId",
                table: "Grades",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Occupations_Departments_DepartmentId",
                table: "Occupations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
