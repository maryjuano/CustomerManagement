using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDriving.Migrations
{
    public partial class Sched_Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schedules_ScheduleId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ScheduleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_StudentId",
                table: "Schedules",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Students_StudentId",
                table: "Schedules",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Students_StudentId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_StudentId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Schedules");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ScheduleId",
                table: "Students",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schedules_ScheduleId",
                table: "Students",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
