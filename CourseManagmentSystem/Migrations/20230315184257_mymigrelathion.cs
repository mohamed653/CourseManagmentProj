using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagmentSystem.Migrations
{
    public partial class mymigrelathion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryId",
                table: "Course",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Category_CategoryId",
                table: "Course",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Category_CategoryId",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_CategoryId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Course");
        }
    }
}
