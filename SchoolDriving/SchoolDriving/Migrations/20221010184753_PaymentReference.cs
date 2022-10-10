using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDriving.Migrations
{
    public partial class PaymentReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentReference",
                table: "Enrollment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentReference",
                table: "Enrollment");
        }
    }
}
