using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDriving.Migrations
{
    public partial class EnrollmentReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Enrollment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Enrollment");
        }
    }
}
