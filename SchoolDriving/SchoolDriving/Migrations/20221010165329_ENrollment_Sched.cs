using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDriving.Migrations
{
    public partial class ENrollment_Sched : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Enrollment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ScheduleId",
                table: "Enrollment",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Schedules_ScheduleId",
                table: "Enrollment",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Schedules_ScheduleId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_ScheduleId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Enrollment");
        }
    }
}
