using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class cabinets2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabinets_Hours_SubjectId",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Cabinets_SubjectId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Classrooms");

            migrationBuilder.AddColumn<int>(
                name: "SubjectType",
                table: "Classrooms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectType",
                table: "Classrooms");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Classrooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_SubjectId",
                table: "Classrooms",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinets_Hours_SubjectId",
                table: "Classrooms",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
