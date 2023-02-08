using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class NullableCabinets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabinets_Hours_SubjectId",
                table: "Classrooms");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Classrooms",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinets_Hours_SubjectId",
                table: "Classrooms",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabinets_Hours_SubjectId",
                table: "Classrooms");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Classrooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinets_Hours_SubjectId",
                table: "Classrooms",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
