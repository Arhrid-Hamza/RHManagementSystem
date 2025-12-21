using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class MakeStartEndDateNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentResponsibleNavigationDepartmentId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_EmployeeResponsibleNavigationEmployeeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentResponsibleNavigationDepartmentId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_EmployeeResponsibleNavigationEmployeeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DepartmentResponsibleNavigationDepartmentId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployeeResponsibleNavigationEmployeeId",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentResponsible",
                table: "Projects",
                column: "DepartmentResponsible");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EmployeeResponsible",
                table: "Projects",
                column: "EmployeeResponsible");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentResponsible",
                table: "Projects",
                column: "DepartmentResponsible",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_EmployeeResponsible",
                table: "Projects",
                column: "EmployeeResponsible",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Departments_DepartmentResponsible",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_EmployeeResponsible",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DepartmentResponsible",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_EmployeeResponsible",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentResponsibleNavigationDepartmentId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeResponsibleNavigationEmployeeId",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DepartmentResponsibleNavigationDepartmentId",
                table: "Projects",
                column: "DepartmentResponsibleNavigationDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EmployeeResponsibleNavigationEmployeeId",
                table: "Projects",
                column: "EmployeeResponsibleNavigationEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Departments_DepartmentResponsibleNavigationDepartmentId",
                table: "Projects",
                column: "DepartmentResponsibleNavigationDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_EmployeeResponsibleNavigationEmployeeId",
                table: "Projects",
                column: "EmployeeResponsibleNavigationEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
