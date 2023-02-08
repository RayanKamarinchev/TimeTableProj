using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class subjectCell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectCellId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Timetable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectType = table.Column<int>(nullable: false),
                    ColorCode = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    TimesIn = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    PositionInDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetable", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Timetable_SubjectCellId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "Timetable");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SubjectCellId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SubjectCellId",
                table: "Teachers");
        }
    }
}
