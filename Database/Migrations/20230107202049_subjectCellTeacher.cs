using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class subjectCellTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Timetable_SubjectCellId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SubjectCellId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SubjectCellId",
                table: "Teachers");

            migrationBuilder.CreateTable(
                name: "SubjectCellTeacher",
                columns: table => new
                {
                    SubjectCellId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCellTeacher", x => new { x.SubjectCellId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_SubjectCellTeacher_Timetable_SubjectCellId",
                        column: x => x.SubjectCellId,
                        principalTable: "Timetable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectCellTeacher_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCellTeacher_TeacherId",
                table: "SubjectCellTeacher",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectCellTeacher");

            migrationBuilder.AddColumn<int>(
                name: "SubjectCellId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SubjectCellId",
                table: "Teachers",
                column: "SubjectCellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Timetable_SubjectCellId",
                table: "Teachers",
                column: "SubjectCellId",
                principalTable: "Timetable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
