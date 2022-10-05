using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDriving.Migrations
{
    public partial class CourseId_Enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Enrollment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Enrollment");
        }
    }
}
