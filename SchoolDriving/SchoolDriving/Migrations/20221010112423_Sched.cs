using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDriving.Migrations
{
    public partial class Sched : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
