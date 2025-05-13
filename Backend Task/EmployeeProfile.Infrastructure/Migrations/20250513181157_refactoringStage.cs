using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProfile.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactoringStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOccupation_Departments_DepartmentId",
                table: "DepartmentOccupation");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOccupation_Occupations_OccupationID",
                table: "DepartmentOccupation");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Grades_GradeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Occupations_OccupationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationGrade_Grades_GradeId",
                table: "OccupationGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationGrade_Occupations_OccupationId",
                table: "OccupationGrade");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GradeId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OccupationId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccupationGrade",
                table: "OccupationGrade");

            migrationBuilder.DropIndex(
                name: "IX_OccupationGrade_OccupationId",
                table: "OccupationGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentOccupation",
                table: "DepartmentOccupation");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentOccupation_OccupationID",
                table: "DepartmentOccupation");

            migrationBuilder.RenameTable(
                name: "OccupationGrade",
                newName: "OccupationGrades");

            migrationBuilder.RenameTable(
                name: "DepartmentOccupation",
                newName: "DepartmentOccupations");

            migrationBuilder.RenameColumn(
                name: "OccupationID",
                table: "DepartmentOccupations",
                newName: "OccupationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccupationGrades",
                table: "OccupationGrades",
                columns: new[] { "OccupationId", "GradeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentOccupations",
                table: "DepartmentOccupations",
                columns: new[] { "OccupationId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_OccupationGrades_GradeId",
                table: "OccupationGrades",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOccupations_DepartmentId",
                table: "DepartmentOccupations",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOccupations_Departments_DepartmentId",
                table: "DepartmentOccupations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOccupations_Occupations_OccupationId",
                table: "DepartmentOccupations",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationGrades_Grades_GradeId",
                table: "OccupationGrades",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationGrades_Occupations_OccupationId",
                table: "OccupationGrades",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOccupations_Departments_DepartmentId",
                table: "DepartmentOccupations");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentOccupations_Occupations_OccupationId",
                table: "DepartmentOccupations");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationGrades_Grades_GradeId",
                table: "OccupationGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationGrades_Occupations_OccupationId",
                table: "OccupationGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OccupationGrades",
                table: "OccupationGrades");

            migrationBuilder.DropIndex(
                name: "IX_OccupationGrades_GradeId",
                table: "OccupationGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentOccupations",
                table: "DepartmentOccupations");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentOccupations_DepartmentId",
                table: "DepartmentOccupations");

            migrationBuilder.RenameTable(
                name: "OccupationGrades",
                newName: "OccupationGrade");

            migrationBuilder.RenameTable(
                name: "DepartmentOccupations",
                newName: "DepartmentOccupation");

            migrationBuilder.RenameColumn(
                name: "OccupationId",
                table: "DepartmentOccupation",
                newName: "OccupationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OccupationGrade",
                table: "OccupationGrade",
                columns: new[] { "GradeId", "OccupationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentOccupation",
                table: "DepartmentOccupation",
                columns: new[] { "DepartmentId", "OccupationID" });

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
                name: "IX_OccupationGrade_OccupationId",
                table: "OccupationGrade",
                column: "OccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentOccupation_OccupationID",
                table: "DepartmentOccupation",
                column: "OccupationID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOccupation_Departments_DepartmentId",
                table: "DepartmentOccupation",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentOccupation_Occupations_OccupationID",
                table: "DepartmentOccupation",
                column: "OccupationID",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationGrade_Grades_GradeId",
                table: "OccupationGrade",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationGrade_Occupations_OccupationId",
                table: "OccupationGrade",
                column: "OccupationId",
                principalTable: "Occupations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
