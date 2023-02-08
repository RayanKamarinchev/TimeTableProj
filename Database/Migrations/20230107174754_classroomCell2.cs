using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class classroomCell2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassroomArrangement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomKey = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    PositionInDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomArrangement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassroomArrangement_Classrooms_ClassroomKey",
                        column: x => x.ClassroomKey,
                        principalTable: "Classrooms",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassroomArrangement_ClassroomKey",
                table: "ClassroomArrangement",
                column: "ClassroomKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassroomArrangement");
        }
    }
}
