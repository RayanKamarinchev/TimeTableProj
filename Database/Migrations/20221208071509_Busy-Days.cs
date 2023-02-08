using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class BusyDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "TeacherBusies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "TeacherBusies");
        }
    }
}
