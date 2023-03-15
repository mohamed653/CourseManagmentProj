using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagmentSystem.Migrations
{
    public partial class CourseLessonsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "CourseLesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseLesson_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLesson_CourseId",
                table: "CourseLesson",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLesson");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
