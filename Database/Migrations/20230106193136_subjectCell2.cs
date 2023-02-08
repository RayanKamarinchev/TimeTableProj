using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class subjectCell2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Timetable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_ClassId",
                table: "Timetable",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timetable_Classes_ClassId",
                table: "Timetable",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timetable_Classes_ClassId",
                table: "Timetable");

            migrationBuilder.DropIndex(
                name: "IX_Timetable_ClassId",
                table: "Timetable");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Timetable");
        }
    }
}
