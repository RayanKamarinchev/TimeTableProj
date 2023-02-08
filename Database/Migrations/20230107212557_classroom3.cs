using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class classroom3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "ClassroomArrangement",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomArrangement_ClassId",
                table: "ClassroomArrangement",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassroomArrangement_Classes_ClassId",
                table: "ClassroomArrangement",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassroomArrangement_Classes_ClassId",
                table: "ClassroomArrangement");

            migrationBuilder.DropIndex(
                name: "IX_ClassroomArrangement_ClassId",
                table: "ClassroomArrangement");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassroomArrangement");
        }
    }
}
